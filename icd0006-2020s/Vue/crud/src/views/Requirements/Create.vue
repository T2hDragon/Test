<template>
    <h1>Edit Requirement</h1>
    <div class="row">
        <div class="col-sm-1 col-md-3"></div>
        <div class="col-sm-10 col-md-4">
            <form>
                <div class="form-group">
                    <label for="Input_Requirement_Name">Name</label>
                    <input
                        class="form-control"
                        type="text"
                        id="Input_Requirement_Name"
                        name="Input_Requirement_Name"
                        v-model.lazy="requirement.name"
                    />
                    <label for="Input_Requirement_Description"
                        >Description</label
                    >
                    <input
                        class="form-control"
                        type="text"
                        id="Input_Requirement_Description"
                        name="Input_Requirement_Description"
                        v-model.lazy="requirement.description"
                    />
                    <label for="Input_Requirement_Price">Price</label>
                    <input
                        class="form-control"
                        type="text"
                        id="Input_Requirement_Price"
                        name="Input_Requirement_Price"
                        v-model.lazy="requirement.price"
                    />
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
            name: 'RequirementsIndex',
        }"
        >Back to list</router-link
    >
</template>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { IRequirement } from "@/domain/IRequirement";
import { BaseService } from "@/services/BaseService";
import { ApiUrls } from "@/services/ApiUrls";

@Options({
    components: {},
    props: { id: String },
})
export default class Create extends Vue {
    requirement = {
        id: "",
        name: "",
        description: "",
        price: 0,
        amount: null,
    };

    requirementService!: BaseService<IRequirement>;

    id!: string;
    mounted(): void {
        this.requirementService = new BaseService(
            ApiUrls.apiBaseUrl + "Requirements"
        );
    }

    async createClick(): Promise<void> {
        const dto = {
            name: this.requirement.name,
            description: this.requirement.description,
            price: this.requirement.price,
            amount: this.requirement.amount,
        };
        this.requirementService.create(dto).finally(() => {
            this.$router.push({ name: "RequirementsIndex" });
        });
    }
}
</script>
