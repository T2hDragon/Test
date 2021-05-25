import React, { useContext } from "react";
import { NavLink } from "react-router-dom";
import { AppContext } from "../context/AppContext";

const Header = () => {
    const appState = useContext(AppContext);
    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div className="container">
                    <NavLink className="navbar-brand" to="/">React Demo</NavLink>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">

                            <li className="nav-item">
                                <NavLink className="nav-link text-dark" to="/titles">Titles</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link text-dark" to="/statuses">Statuses</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link text-dark" to="/requirements">Requirements</NavLink>
                            </li>

                            {/* <li className="nav-item dropdown">
                                <a className="nav-link dropdown-toggle text-dark" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Languages</a>
                                <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <a className="dropdown-item text-dark" href="/Home/SetLanguage?culture=et&amp;returnUrl=%2F">eesti</a>
                                    <a className="dropdown-item text-dark" href="/Home/SetLanguage?culture=en-GB&amp;returnUrl=%2F">English (United Kingdom)</a>
                                </div>
                            </li> */}

                        </ul>
                        <ul className="navbar-nav">
                            {appState.jwt === null ?
                                <>
                                    <li className="nav-item">
                                        <NavLink className="nav-link d-block text-dark" to="/identity/login">Login</NavLink>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink className="nav-link d-block text-dark" to="/identity/register">Register</NavLink>
                                    </li>
                                </>
                                :
                                <>
                                    <li className="nav-item">
                                        <span className="nav-link text-dark">{appState.firstName + ' ' + appState.lastName}</span>
                                    </li>
                                    <li className="nav-item">
                                        <button onClick={() => appState.setAuthInfo(null, '', '', '')} className="btn btn-link nav-link text-dark" >Logout</button>
                                    </li>
                                </>
                            }

                        </ul>

                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Header;