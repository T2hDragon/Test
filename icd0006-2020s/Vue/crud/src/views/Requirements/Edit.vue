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
                    <label
                        for="Input_Requirement_Amount"
                        v-if="requirement.amount !== null"
                        >Amount</label
                    >
                    <input
                        class="form-control"
                        type="text"
                        id="Input_Requirement_Amount"
                        name="Input_Requirement_Amount"
                        v-if="requirement.amount !== null"
                        v-model.number="requirement.amount"
                    />
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
            name: 'RequirementsIndex',
            params: { id: requirement.id },
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
export default class Edit extends Vue {
    requirement: IRequirement = {
        id: "",
        name: "",
        description: "",
        price: 0,
        amount: -1,
    };

    requirementService!: BaseService<IRequirement>;

    id!: string;
    mounted(): void {
        this.requirementService = new BaseService(
            ApiUrls.apiBaseUrl + "Requirements"
        );
        this.requirementService.get(this.id).then((data) => {
            if (data.data != null) {
                this.requirement = data.data;
            }
        });
    }

    async updateClick(): Promise<void> {
        const dto: IRequirement = {
            id: this.requirement.id,
            name: this.requirement.name,
            description: this.requirement.description,
            price: this.requirement.price,
            amount: this.requirement.amount,
        };
        this.requirementService.update(this.id, dto).finally(() => {
            this.$router.push({ name: "RequirementsIndex" });
        });
    }

    async deleteClick(): Promise<void> {
        this.requirementService.delete(this.id).finally(() => {
            this.$router.push({ name: "RequirementsIndex" });
        });
    }
}
</script>
