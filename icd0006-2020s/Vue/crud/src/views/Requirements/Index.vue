<template>
    <h1>Requirements</h1>
    <router-link
        :to="{
            name: 'RequirementsCreate',
        }"
        >Create new</router-link
    >
    <br /><br />
    <table class="table">
        <thead>
            <tr>
                <th class="col-3">Name</th>
                <th class="col-3">Description</th>
                <th class="col-3">Price</th>
                <th class="col-3"></th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="requirement in requirements" v-bind:key="requirement.id">
                <td>
                    {{ requirement.name }}
                </td>
                <td>
                    {{ requirement.description }}
                </td>
                <td>
                    {{ requirement.price }}
                </td>
                <td class="float-right">
                    <router-link
                        :to="{
                            name: 'RequirementsEdit',
                            params: { id: requirement.id },
                        }"
                        >Edit</router-link
                    >
                    |
                    <router-link
                        :to="{
                            name: 'RequirementsDetails',
                            params: { id: requirement.id },
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
import { IRequirement } from "@/domain/IRequirement";
import { BaseService } from "@/services/BaseService";
import { ApiUrls } from "@/services/ApiUrls";

@Options({
    components: {},
    props: {},
})
export default class Index extends Vue {
    requirements: IRequirement[] = [];
    mounted(): void {
        const requirementsService: BaseService<IRequirement> = new BaseService(
            ApiUrls.apiBaseUrl + "Requirements/"
        );
        requirementsService.getAll().then((data) => {
            if (data.data != null) {
                this.requirements = data.data;
            }
        });
    }
}
</script>
