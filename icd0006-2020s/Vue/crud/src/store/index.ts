import { createStore } from 'vuex';
import axios from 'axios';
import { IJwt } from '../types/IJwt';
import { ILoginInfo } from '../types/ILoginInfo';
import { IRegisterInfo } from '../types/IRegisterInfo';
import { IState } from '../state/IState';
import { ApiUrls } from "../services/ApiUrls"
import { AccountService } from "../services/AccountService"

export const initialState: IState = {
    token: null,
    username: '',
    firstname: '',
    lastname: '',
}

export default createStore({
    state: initialState,
    mutations: {
        logOut: (state: IState) => {
            state.token = null;
            state.username = '';
            state.firstname = '';
            state.lastname = '';
        },
        logIn: (state: IState, jwtResponse: IJwt) => {
            state.token = jwtResponse.token;
            state.username = jwtResponse.userName;
            state.firstname = jwtResponse.firstname;
            state.lastname = jwtResponse.lastname;
        },
    },
    actions: {
        logout(context): void {
            context.commit("logOut")
        },
        async logIn(context, login: ILoginInfo): Promise<boolean> {
            const loginDataStr = JSON.stringify(login);
            const response = await axios.post(
                ApiUrls.apiBaseUrl + ApiUrls.login,
                loginDataStr,
                { headers: { 'Content-type': 'application/json' } }
            );
            if (response.status === 200) {
                context.commit('logIn', response.data);
                return true;
            }
            return false;
        },
        async register(context, registerInfo: IRegisterInfo): Promise<boolean> {
            const response = await AccountService.registerUser(registerInfo)
            let passed = response !== null
            if (passed) {
                passed = await context.dispatch("logIn", {
                    username: (registerInfo as any).userName,
                    password: registerInfo.password,
                });
            }
            return passed;
        }
    },
    getters: {
        isLoggedIn(context): boolean {
            return context.token !== null
        },
        loggedInUserName(context): string | null {
            return context.username
        },
        jwt(context): string | null {
            return context.token
        },
    },
    modules: {
    }
})
