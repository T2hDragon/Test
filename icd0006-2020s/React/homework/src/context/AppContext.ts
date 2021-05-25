import React from "react";

export interface IAppState {
    jwt: string | null;
    userName: string;
    firstName: string;
    lastName: string;
    setAuthInfo: (jwt: string | null, username: string, firstName: string, lastName: string) => void;
}

export const initialAppState : IAppState = {
    jwt: null,
    userName: '',
    firstName: '',
    lastName: '',
    setAuthInfo: (jwt: string | null, firstName: string, lastName: string): void => {}
}

export const AppContext = React.createContext<IAppState>(initialAppState);
export const AppContextProvider = AppContext.Provider;
export const AppContextConsumer = AppContext.Consumer;
