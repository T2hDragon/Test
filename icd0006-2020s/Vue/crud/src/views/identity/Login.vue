<template>
    <div class="row">
        <div class="col-sm-1 col-md-3"></div>
        <div class="col-sm-10 col-md-6">
            <h4>Use a local account to log in.</h4>
            <hr />
            <div class="form-group">
                <label for="Input_Username">Username</label>
                <input
                    class="form-control"
                    type="text"
                    id="Input_Username"
                    v-model="username"
                />
            </div>
            <div class="form-group">
                <label for="Input_Password">Password</label>
                <input
                    class="form-control"
                    type="password"
                    id="Input_Password"
                    v-model="password"
                />
            </div>
            <div class="form-group">
                <button
                    @click="loginClicked($event)"
                    type="submit"
                    class="btn btn-primary"
                >
                    Log in
                </button>
            </div>
        </div>
        <div class="col-sm-1 col-md-3"></div>
    </div>
</template>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import store from "@/store";

@Options({
    components: {},
    props: {},
})
export default class Login extends Vue {
    username: string = "admin";
    password: string = "DucksG0Quack!";

    async loginClicked(): Promise<void> {
        const passed: boolean = await store.dispatch("logIn", {
            username: this.username,
            password: this.password,
        });
        if (passed) {
            this.$router.push("/");
        }
    }
}
</script>
