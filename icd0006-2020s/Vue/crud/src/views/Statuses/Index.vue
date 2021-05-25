<template>
    <h1>Statuses</h1>
    <router-link
        :to="{
            name: 'StatusesCreate',
        }"
        >Create new</router-link
    >
    <br /><br />
    <table class="table">
        <thead>
            <tr>
                <th>Name</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="status in statuses" v-bind:key="status.id">
                <td>
                    {{ status.name }}
                </td>
                <td class="float-right">
                    <router-link
                        :to="{
                            name: 'StatusesEdit',
                            params: { id: status.id },
                        }"
                        >Edit</router-link
                    >
                    |
                    <router-link
                        :to="{
                            name: 'StatusesDetails',
                            params: { id: status.id },
                        }"
                        >Details</router-link
                    >
                </td>
            </tr>
        </tbody>
    </table>
</template>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { IStatus } from "@/domain/IStatus";
import { BaseService } from "@/services/BaseService";
import { ApiUrls } from "@/services/ApiUrls";

@Options({
    components: {},
    props: {},
})
export default class Index extends Vue {
    statuses: IStatus[] = [];
    mounted(): void {
        const statusService: BaseService<IStatus> = new BaseService(
            ApiUrls.apiBaseUrl + "Statuses/"
        );
        statusService.getAll().then((data) => {
            if (data.data != null) {
                this.statuses = data.data;
            }
        });
    }
}
</script>
