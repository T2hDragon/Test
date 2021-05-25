<template>
    <h1>Edit Title</h1>
    <div class="row">
        <div class="col-sm-1 col-md-3"></div>
        <div class="col-sm-10 col-md-4">
            <form>
                <div class="form-group">
                    <label for="Input_Title">Title</label>
                    <input
                        class="form-control"
                        type="text"
                        id="Input_Title"
                        name="Input_Title"
                        v-model.lazy="title.name"
                    />
                    <span class="text-danger field-validation-valid"></span>
                </div>

                <button
                    type="submit"
                    v-on:click.prevent="createClick"
                    class="btn btn-primary"
                >
                    Create
                </button>
            </form>
        </div>
        <div class="col-sm-1 col-md-3"></div>
    </div>
    <router-link
        :to="{
            name: 'TitlesIndex',
        }"
        >Back to list</router-link
    >
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
export default class Create extends Vue {
    title: ITitle = { id: "", name: "" };
    titleService!: BaseService<ITitle>;
    id!: string;
    mounted(): void {
        this.titleService = new BaseService(ApiUrls.apiBaseUrl + "Titles");
    }

    async createClick(): Promise<void> {
        const dto = {
            name: this.title.name,
        };
        this.titleService.create(dto).finally(() => {
            this.$router.push({ name: "TitlesIndex" });
        });
    }
}
</script>
