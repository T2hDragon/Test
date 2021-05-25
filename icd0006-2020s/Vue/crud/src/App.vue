<template>
    <div id="app">
        <div class="container">
            <div class="jumbotron shadow rounded bg-white">
                <header>
                    <NavBar />
                </header>
                <div class="container">
                    <main role="main" class="pb-3">
                        <router-view></router-view>
                    </main>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import NavBar from "./components/NavBar.vue";
import store from "@/store";

@Options({
    // Do not forget to register imported components!!!!!
    components: {
        NavBar,
    },
})
export default class App extends Vue {
    get token(): string | null {
        return store.state.token;
    }

    get firstname(): string {
        return store.state.firstname;
    }

    get username(): string {
        return store.state.username;
    }

    get lastname(): string {
        return store.state.lastname;
    }

    logOut(): void {
        store.commit("logOut");
        this.$router.push("/");
    }
}
</script>

<style>
/* Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
for details on configuring this project to bundle and minify static web assets. */

a.navbar-brand {
    white-space: normal;
    text-align: center;
    word-break: break-all;
}

/* Provide sufficient contrast against white background */
a {
    color: #0366d6;
}

.btn-primary {
    color: #fff;
    background-color: #1b6ec2;
    border-color: #1861ac;
}

.bg-purple {
    background-color: #615 !important;
}

.nav-pills .nav-link.active,
.nav-pills .show > .nav-link {
    color: #fff;
    background-color: #1b6ec2;
    border-color: #1861ac;
}

/* Sticky footer styles
-------------------------------------------------- */
html {
    font-size: 14px;
}
@media (min-width: 768px) {
    html {
        font-size: 16px;
    }
}

.border-top {
    border-top: 1px solid #e5e5e5;
}
.border-bottom {
    border-bottom: 1px solid #e5e5e5;
}

.box-shadow {
    box-shadow: 0 0.25rem 0.75rem rgba(0, 0, 0, 0.05);
}

button.accept-policy {
    font-size: 1rem;
    line-height: inherit;
}

/* Sticky footer styles
-------------------------------------------------- */
html {
    position: relative;
    min-height: 100%;
}

body {
    /* Margin bottom by footer height */
    margin-bottom: 60px;
    background-color: #e7fffc !important;
}
.footer {
    position: absolute;
    bottom: 0;
    width: 100%;
    white-space: nowrap;
    line-height: 60px; /* Vertically center the text there */
}
</style>
