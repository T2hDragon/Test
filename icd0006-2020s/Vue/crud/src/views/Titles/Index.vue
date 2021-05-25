<template>
    <h1>Titles</h1>
    <router-link
        :to="{
            name: 'TitlesCreate',
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
            <tr v-for="title in titles" v-bind:key="title.id">
                <td>
                    {{ title.name }}
                </td>
                <td class="float-right">
                    <router-link
                        :to="{
                            name: 'TitlesEdit',
                            params: { id: title.id },
                        }"
                        >Edit</router-link
                    >
                    |
                    <router-link
                        :to="{
                            name: 'TitlesDetails',
                            params: { id: title.id },
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
import { ITitle } from "@/domain/ITitle";
import { BaseService } from "@/services/BaseService";
import { ApiUrls } from "@/services/ApiUrls";

@Options({
    components: {},
    props: {},
})
export default class Index extends Vue {
    titles: ITitle[] = [];
    mounted(): void {
        const titleService: BaseService<ITitle> = new BaseService(
            ApiUrls.apiBaseUrl + "Titles/"
        );
        titleService.getAll().then((data) => {
            if (data.data != null) {
                this.titles = data.data;
            }
        });
    }
}
</script>
