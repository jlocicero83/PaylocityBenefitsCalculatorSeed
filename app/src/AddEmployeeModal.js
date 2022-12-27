import { useState } from "react";
import { baseUrl } from "./Constants";

const AddEmployeeModal = (props) => { 
    const [newEmployee, setInputs] = useState({});
    // eslint-disable-next-line no-unused-vars
    const [error, setError] = useState(null);
    


async function addEmployee(url, newEmployee) {
    const response = await fetch(url, {
      method: 'POST', 
      headers: {
        "access-control-allow-origin" : "*",
        "Content-type": "application/json; charset=UTF-8"
      },
       body: JSON.stringify(newEmployee),
    });
    return await response.json(); 
  }

    const handleChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({...values, [name]: value}))
        }

    const handleSubmit = (event) => {
        event.preventDefault();
        addEmployee(`${baseUrl}/api/v1/employees`, newEmployee)
            .then((response) => {
                if(response.success){
                    setError(null);
                    
                    //**Temporary fix -- see TODO**/
                    window.location.reload(false);
                      
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
        <div className="modal fade" id="add-employee-modal" tabIndex="-1" aria-labelledby="add-employee-modal-label" aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5" id="add-employee-modal-label">Add Employee</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                         {/* React is relatively new for me so I am hardcoding the form here for the sake of time. I would normally
                         create a form component that could be used here but I'm having trouble wiring up the submit through the save changes
                         button.*/}
                         <form>
                            <div className="form-group">
                                <label htmlFor="firstName">First Name</label>
                                <input 
                                    className="form-control" 
                                    type="text" 
                                    name="firstName" 
                                    value={newEmployee.firstName || ""} 
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="lastName">Last Name</label>
                                <input 
                                    className="form-control" 
                                    type="text" 
                                    name="lastName" 
                                    value={newEmployee.lastName || ""} 
                                    onChange={handleChange}
                                />            
                            </div>
                            <div className="form-group">
                                <label htmlFor="salary">Salary</label>
                                <input 
                                    className="form-control" 
                                    type="number" 
                                    name="salary" 
                                    value={newEmployee.salary || ""} 
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="dateOfBirth">Date of Birth</label>
                                <input 
                                    className="form-control" 
                                    type="date" 
                                    name="dateOfBirth" 
                                    value={newEmployee.dateOfBirth || ""} 
                                    onChange={handleChange}
                                />            
                            </div>
                        </form>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" className="btn btn-primary" onClick={handleSubmit} data-bs-dismiss="modal">Save changes</button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AddEmployeeModal;