import { useState } from "react";

const AddEmployeeModal = (props) => { 
    const [inputs, setInputs] = useState({});

    const handleChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({...values, [name]: value}))
        }

    const handleSubmit = (event) => {
        event.preventDefault();
        
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
                                    value={inputs.firstName || ""} 
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="lastName">Last Name</label>
                                <input 
                                    className="form-control" 
                                    type="text" 
                                    name="lastName" 
                                    value={inputs.lastName || ""} 
                                    onChange={handleChange}
                                />            
                            </div>
                            <div className="form-group">
                                <label htmlFor="salary">Salary</label>
                                <input 
                                    className="form-control" 
                                    type="number" 
                                    name="salary" 
                                    value={inputs.salary || ""} 
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="dateOfBirth">Date of Birth</label>
                                <input 
                                    className="form-control" 
                                    type="date" 
                                    name="dateOfBirth" 
                                    value={inputs.dateOfBirth || ""} 
                                    onChange={handleChange}
                                />            
                            </div>
                        </form>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" className="btn btn-primary" onClick={handleSubmit}>Save changes</button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AddEmployeeModal;