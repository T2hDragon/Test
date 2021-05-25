<template>
    <div class="row">
        <div class="col-sm-1 col-md-3"></div>
        <div class="col-sm-10 col-md-6">
            <h4>Register a local account.</h4>
            <hr />
            <div class="form-group">
                <label for="Input_Email">Email</label>
                <input
                    class="form-control"
                    type="email"
                    id="Input_Email"
                    v-model="email"
                />
            </div>
            <div class="form-group">
                <label for="Input_UserName">Username</label>
                <input
                    class="form-control"
                    type="text"
                    id="Input_UserName"
                    v-model="userName"
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
                <label for="Input_FirstName">First Name</label>
                <input
                    class="form-control"
                    type="text"
                    id="Input_FirstName"
                    v-model="firstname"
                />
            </div>
            <div class="form-group">
                <label for="Input_LastName">Last Name</label>
                <input
                    class="form-control"
                    type="text"
                    id="Input_LastName"
                    v-model="lastname"
                />
            </div>
            <div class="form-group">
                <button
                    @click="registerClicked($event)"
                    type="submit"
                    class="btn btn-primary"
                >
                    Register
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
    email: string = "kaalte@ttu.ee";
    password: string = "DucksG0Quack!";
    userName: string = "admin";
    firstname: string = "Karmo";
    lastname: string = "Alteberg";

    async registerClicked(): Promise<void> {
        const passed: boolean = await store.dispatch("register", {
            email: this.email,
            password: this.password,
            userName: this.userName,
            firstname: this.firstname,
            lastname: this.lastname,
        });

        if (passed) {
            this.$router.push("/");
        }
    }
}
</script>
