<template>
    <h1>Details</h1>

    <div>
        <h4>Requirement</h4>
        <hr />
        <dl class="row">
            <dt class="col-sm-4">Id</dt>
            <dd class="col-sm-8">
                {{ id }}
            </dd>
            <dt class="col-sm-4">Name</dt>
            <dd class="col-sm-8">
                {{ requirement.name }}
            </dd>
            <dt class="col-sm-4">Description</dt>
            <dd class="col-sm-8">
                {{ requirement.description }}
            </dd>
            <dt class="col-sm-4">Price</dt>
            <dd class="col-sm-8">
                {{ requirement.price }}
            </dd>

            <dt v-if="requirement.amount !== null" class="col-sm-4">Amount</dt>
            <dd v-if="requirement.amount !== null" class="col-sm-8">
                {{ requirement.amount }}
            </dd>
        </dl>
    </div>
    <div>
        <router-link
            :to="{
                name: 'RequirementsIndex',
            }"
            aria-haspopup="true"
            aria-expanded="false"
            >Back to list</router-link
        >
    </div>
</template>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { IRequirement } from "@/domain/IRequirement";
import { BaseService } from "@/services/BaseService";
import { ApiUrls } from "@/services/ApiUrls";

@Options({
    components: {},
    props: {
        id: String,
    },
})
export default class Details extends Vue {
    requirement: IRequirement = {
        id: "",
        name: "",
        description: "",
        price: 0,
        amount: -1,
    };

    id!: string;

    mounted(): void {
        const requirementService: BaseService<IRequirement> = new BaseService(
            ApiUrls.apiBaseUrl + "Requirements"
        );
        requirementService.get(this.id).then((data) => {
            if (data.data != null) {
                this.requirement = data.data;
            }
        });
    }
}
</script>
