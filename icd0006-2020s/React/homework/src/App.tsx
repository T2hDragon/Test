import React, { useState } from 'react';
import { Route, Switch } from 'react-router-dom';
import Footer from './components/Footer';
import Header from './components/Header';
import HomeIndex from './containers/home/HomeIndex';
import Login from './containers/identity/Login';
import Register from './containers/identity/Register';
import Page404 from './containers/Page404';
import PageForm from './containers/PageForm';
import { AppContextProvider, initialAppState } from './context/AppContext';
import { Index as TitleIndex } from './containers/titles/Index';
import { Details as TitleDetails } from './containers/titles/Details';
import { Edit as TitleEdit } from './containers/titles/Edit';
import { Create as TitleCreate } from './containers/titles/Create';

import { Index as StatusIndex } from './containers/statuses/Index';
import { Details as StatusDetails } from './containers/statuses/Details';
import { Edit as StatusEdit } from './containers/statuses/Edit';
import { Create as StatusCreate } from './containers/statuses/Create';

import { Index as RequirementIndex } from './containers/requirements/Index';
import { Details as RequirementDetails } from './containers/requirements/Details';
import { Edit as RequirementEdit } from './containers/requirements/Edit';
import { Create as RequirementCreate } from './containers/requirements/Create';

function App() {
    const setAuthInfo = (jwt: string | null, firstName: string, lastName: string): void => {
        setAppState({ ...appState, jwt, firstName, lastName });
    }

    const [appState, setAppState] = useState({ ...initialAppState, setAuthInfo });

    return (
        <>
            <AppContextProvider value={appState} >
                <Header />
                <div className="container">
                    <main role="main" className="pb-3">
                        <Switch>
                            <Route exact path="/" component={HomeIndex} />

                            <Route path="/form" component={PageForm} />

                            <Route path="/identity/login" component={Login} />
                            <Route path="/identity/register" component={Register} />

                            <Route path="/titles/create" component={TitleCreate} />
                            <Route path="/titles/edit/:id" component={TitleEdit} />
                            <Route path="/titles/:id" component={TitleDetails} />
                            <Route path="/titles" component={TitleIndex} />

                            <Route path="/statuses/create" component={StatusCreate} />
                            <Route path="/statuses/edit/:id" component={StatusEdit} />
                            <Route path="/statuses/:id" component={StatusDetails} />
                            <Route path="/statuses" component={StatusIndex} />

                            <Route path="/requirements/create" component={RequirementCreate} />
                            <Route path="/requirements/edit/:id" component={RequirementEdit} />
                            <Route path="/requirements/:id" component={RequirementDetails} />
                            <Route path="/requirements" component={RequirementIndex} />

                            <Route component={Page404} />
                        </Switch>
                    </main>
                </div>
                <Footer />
            </AppContextProvider>
        </>
    );
}

export default App;
