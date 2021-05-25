<template>
    <h1>Details</h1>

    <div>
        <h4>Status</h4>
        <hr />
        <dl class="row">
            <dt class="col-sm-4">Id</dt>
            <dd class="col-sm-8">
                {{ id }}
            </dd>
            <dt class="col-sm-4">Name</dt>
            <dd class="col-sm-8">
                {{ status.name }}
            </dd>
        </dl>
    </div>
    <div>
        <router-link
            :to="{
                name: 'StatusesIndex',
            }"
            aria-haspopup="true"
            aria-expanded="false"
            >Back to list</router-link
        >
    </div>
</template>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { IStatus } from "@/domain/IStatus";
import { BaseService } from "@/services/BaseService";
import { ApiUrls } from "@/services/ApiUrls";

@Options({
    components: {},
    props: {
        id: String,
    },
})
export default class Details extends Vue {
    status: IStatus = { id: "", name: "" };
    id!: string;
    mounted(): void {
        const statusService: BaseService<IStatus> = new BaseService(
            ApiUrls.apiBaseUrl + "Statuses"
        );
        statusService.get(this.id).then((data) => {
            if (data.data != null) {
                this.status = data.data;
            }
        });
    }
}
</script>
