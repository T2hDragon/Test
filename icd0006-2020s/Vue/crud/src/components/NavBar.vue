<template>
    <nav
        class="navbar fixed-top navbar-expand-sm navbar-toggleable-sm navbar-dark bg-secondary border-bottom box-shadow mb-3"
    >
        <div class="container">
            <router-link
                to="/"
                class="navbar-brand"
                id="navbarHome"
                aria-haspopup="true"
                aria-expanded="false"
            >
                DrivingSchool homework
            </router-link>
            <button
                class="navbar-toggler"
                type="button"
                data-toggle="collapse"
                data-target="#navbarSupportedContent"
                aria-controls="navbarSupportedContent"
                aria-expanded="false"
                aria-label="Toggle navigation"
            >
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul v-if="isLoggedIn" class="navbar-nav flex-grow-1">
                    <li class="nav-item">
                        <router-link
                            :to="{
                                name: 'TitlesIndex',
                            }"
                            class="nav-link"
                            id="navbarTitles"
                            aria-haspopup="true"
                            aria-expanded="false"
                            >Titles</router-link
                        >
                    </li>
                    <li class="nav-item">
                        <router-link
                            :to="{
                                name: 'StatusesIndex',
                            }"
                            class="nav-link"
                            id="navbarStatuses"
                            aria-haspopup="true"
                            aria-expanded="false"
                            >Statuses</router-link
                        >
                    </li>
                    <li class="nav-item">
                        <router-link
                            :to="{
                                name: 'RequirementsIndex',
                            }"
                            class="nav-link"
                            id="navbarRequirements"
                            aria-haspopup="true"
                            aria-expanded="false"
                            >Requirements</router-link
                        >
                    </li>
                </ul>
                <ul class="navbar-nav ml-auto">
                    <template v-if="isLoggedIn">
                        <li class="nav-item">
                            <router-link
                                to="/"
                                class="nav-link"
                                id="navbarGreeting"
                                aria-haspopup="true"
                                aria-expanded="false"
                            >
                                Hello, {{ loggedInUserName }}!</router-link
                            >
                        </li>
                        <li class="nav-item">
                            <a
                                @click="logoutOnClick"
                                class="nav-link"
                                id="navbarLogout"
                                aria-haspopup="true"
                                aria-expanded="false"
                                href="#"
                            >
                                Log Out
                            </a>
                        </li>
                    </template>
                    <template v-else>
                        <li class="nav-item">
                            <router-link
                                to="/account/login"
                                class="nav-link"
                                id="navbarLogin"
                                aria-haspopup="true"
                                aria-expanded="false"
                            >
                                Log in</router-link
                            >
                        </li>
                        <li class="nav-item">
                            <router-link
                                to="/account/register"
                                class="nav-link"
                                id="navbarRegister"
                                aria-haspopup="true"
                                aria-expanded="false"
                            >
                                Register
                            </router-link>
                        </li>
                    </template>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import store from "../store";
import router from "../router";

@Options({
    components: {},
    props: {},
})
export default class NavBar extends Vue {
    get isLoggedIn(): boolean {
        return store.getters.isLoggedIn;
    }

    get loggedInUserName(): string {
        return store.getters.loggedInUserName;
    }

    logoutOnClick(): void {
        store.dispatch("logout");
        router.push("/");
    }
}
</script>
