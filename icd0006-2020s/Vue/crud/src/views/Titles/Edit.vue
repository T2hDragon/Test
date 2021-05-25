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
                    v-on:click.prevent="updateClick"
                    class="btn btn-primary"
                >
                    Update
                </button>
                <button
                    type="submit"
                    v-on:click.prevent="deleteClick"
                    class="btn btn-danger float-right"
                >
                    Delete
                </button>
            </form>
        </div>
        <div class="col-sm-1 col-md-3"></div>
    </div>
    <router-link
        :to="{
            name: 'TitlesIndex',
            params: { id: title.id }
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
    props: { id: String }
})
export default class Edit extends Vue {
    title: ITitle = { id: "", name: "" };
    titleService!: BaseService<ITitle>;
    id!: string;
    mounted(): void {
        this.titleService = new BaseService(ApiUrls.apiBaseUrl + "Titles");
        this.titleService.get(this.id).then(data => {
            if (data.data != null) {
                this.title = data.data;
            }
        });
    }

    async updateClick(): Promise<void> {
        const dto: ITitle = {
            id: this.title.id,
            name: this.title.name
        };
        this.titleService.update(this.id, dto).finally(() => {
            this.$router.push({ name: "TitlesIndex" });
        });
    }

    async deleteClick(): Promise<void> {
        this.titleService.delete(this.id).finally(() => {
            this.$router.push({ name: "TitlesIndex" });
        });
    }
}
</script>
