import { currencyFormat, dateFormat } from "./Constants";
import { baseUrl } from "./Constants";
import { useState } from "react";

const Employee = (props) => {
    const firstName = props.firstName || '';
    const lastName = props.lastName || '';
    const salary = props.salary || 0;
    const dateOfBirth = props.dateOfBirth || '';
    // eslint-disable-next-line no-unused-vars
    const [error, setError] = useState(null);

    async function deleteEmployee(url) {
        const response = await fetch(url, {
          method: 'DELETE', 
          headers: {
            "access-control-allow-origin" : "*",
            "Content-type": "application/json; charset=UTF-8"
          }
        });
        return response;
      }

    const handleDelete = () =>{

    //TODO: Prompt the user to confirm delete
      deleteEmployee(`${baseUrl}/api/v1/employees/${props.id}`)
      .then((response) => {
        if(response.success){
            setError(null);
             
            //TODO: use context API to refresh the employee listing or somehow pass the getEmployees() method through props?
            //New employee is only showing after refreshing the page. 
        }
    }) 
    .catch((error) => { 
        setError(error);
        alert(error);
    }); 
    }

    return (
        <tr>
            <th scope="row">{props.id}</th>
            <td>{lastName}</td>
            <td>{firstName}</td>
            <td>{dateFormat(dateOfBirth)}</td>
            <td>{currencyFormat(salary)}</td>
            <td>{props.dependents?.length || 0}</td>
            <td>
                <a href="#" data-bs-toggle="modal" data-bs-target={`#${props.editModalId}`}>Edit</a>  <a href="#" onClick={handleDelete}>Delete</a>
            </td>
        </tr>
    );
};

export default Employee;