import React, { useContext, useState } from "react";
import { Redirect } from "react-router";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IdentityService } from "../../services/identity-service";



const Register = () => {
    const appState = useContext(AppContext);

    const [registerData, setRegisterData] = useState({ firstName: '', lastName: '', email: '', userName: '', password: '', passwordConfirmation: '' });
    const [alertMessage, setAlertMessage] = useState('');

    const registerClicked = async (e: Event) => {
        e.preventDefault();
        if (registerData.userName === '' || registerData.password === '') {
            setAlertMessage('Empty username or password!');
        };
        if (registerData.password !== '' || registerData.passwordConfirmation) {
            setAlertMessage("Password don't match");
        };
        setAlertMessage('');
        let registerResponse = await IdentityService.Register('account/register', registerData);
        if (!registerResponse.ok) {
            setAlertMessage(registerResponse.messages![0]);
        } else {
            setAlertMessage('');
            let loginResponse = await IdentityService.Login('account/login', { userName: registerData.userName, password: registerData.password });
            if (!loginResponse.ok) {
                setAlertMessage('Unable to Log In');
            } else {
                appState.setAuthInfo(loginResponse.data!.token, loginResponse.data!.userName, loginResponse.data!.firstname, loginResponse.data!.lastname);
            }
        }
    }

    return (
        <>
            { appState.jwt !== null ? <Redirect to="/" /> : null}
            <h1>Register</h1>
            <form onSubmit={(e) => e.preventDefault}>
                <div className="row">
                    <div className="col-md-6">
                        <section>
                            <hr />
                            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Danger} />
                            <div className="form-group">
                                <label htmlFor="Input_username">Username</label>
                                <input value={registerData.userName} onChange={e => setRegisterData({ ...registerData, userName: e.target.value })} className="form-control" type="text" id="Input_Username" name="Input.Username" placeholder="username" autoComplete="username" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="Input_email">Email</label>
                                <input value={registerData.email} onChange={e => setRegisterData({ ...registerData, email: e.target.value })} className="form-control" type="text" id="Input_email" name="Input.email" placeholder="example@mail.com" autoComplete="email" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="Input_Firstname">Firstname</label>
                                <input value={registerData.firstName} onChange={e => setRegisterData({ ...registerData, firstName: e.target.value })} className="form-control" type="text" id="Input_Firstname" name="Input.Firstname" placeholder="First name" autoComplete="firstname" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="Input_Lastname">Lastname</label>
                                <input value={registerData.lastName} onChange={e => setRegisterData({ ...registerData, lastName: e.target.value })} className="form-control" type="text" id="Input_Lastname" name="Input.Lastname" placeholder="Last name" autoComplete="lastname" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="Input_Password">Password</label>
                                <input value={registerData.password} onChange={e => setRegisterData({ ...registerData, password: e.target.value })} className="form-control" type="password" id="Input_Password" name="Input.Password" placeholder="Input your current password..." autoComplete="current-password" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="Input_Password_Confirmation">Password confirmation</label>
                                <input value={registerData.passwordConfirmation} onChange={e => setRegisterData({ ...registerData, passwordConfirmation: e.target.value })} className="form-control" type="password" id="Input_Password_Confirmation" name="Input.Password_Confirmation" placeholder="Repeat your password" autoComplete="current-password" />
                            </div>
                            <div className="form-group">
                                <button onClick={(e) => registerClicked(e.nativeEvent)} type="submit" className="btn btn-primary">Register</button>
                            </div>
                        </section>
                    </div>
                </div>
            </form>
        </>
    );
}

export default Register;