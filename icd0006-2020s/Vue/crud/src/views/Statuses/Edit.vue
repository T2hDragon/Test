<template>
    <h1>Edit Status</h1>
    <div class="row">
        <div class="col-sm-1 col-md-3"></div>
        <div class="col-sm-10 col-md-4">
            <form>
                <div class="form-group">
                    <label for="Input_Status">Status</label>
                    <input
                        class="form-control"
                        type="text"
                        id="Input_Status"
                        name="Input_Status"
                        v-model.lazy="status.name"
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
            name: 'StatusesIndex',
            params: { id: status.id },
        }"
        >Back to list</router-link
    >
</template>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { IStatus } from "@/domain/IStatus";
import { BaseService } from "@/services/BaseService";
import { ApiUrls } from "@/services/ApiUrls";

@Options({
    components: {},
    props: { id: String },
})
export default class Edit extends Vue {
    status: IStatus = { id: "", name: "" };
    statusService!: BaseService<IStatus>;
    id!: string;
    mounted(): void {
        this.statusService = new BaseService(ApiUrls.apiBaseUrl + "Statuses");
        this.statusService.get(this.id).then((data) => {
            if (data.data != null) {
                this.status = data.data;
            }
        });
    }

    async updateClick(): Promise<void> {
        const dto: IStatus = {
            id: this.status.id,
            name: this.status.name,
        };
        this.statusService.update(this.id, dto).finally(() => {
            this.$router.push({ name: "StatusesIndex" });
        });
    }

    async deleteClick(): Promise<void> {
        this.statusService.delete(this.id).finally(() => {
            this.$router.push({ name: "StatusesIndex" });
        });
    }
}
</script>
