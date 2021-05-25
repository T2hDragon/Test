<template>
    <h1>Details</h1>

    <div>
        <h4>Title</h4>
        <hr />
        <dl class="row">
            <dt class="col-sm-4">Id</dt>
            <dd class="col-sm-8">
                {{ id }}
            </dd>
            <dt class="col-sm-4">Name</dt>
            <dd class="col-sm-8">
                {{ title.name }}
            </dd>
        </dl>
    </div>
    <div>
        <router-link
            :to="{
                name: 'TitlesIndex',
            }"
            aria-haspopup="true"
            aria-expanded="false"
            >Back to list</router-link
        >
    </div>
</template>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { ITitle } from "@/domain/ITitle";
import { BaseService } from "@/services/BaseService";
import { ApiUrls } from "@/services/ApiUrls";

@Options({
    components: {},
    props: {
        id: String,
    },
})
export default class Details extends Vue {
    title: ITitle = { id: "", name: "" };
    id!: string;
    mounted(): void {
        const titleService: BaseService<ITitle> = new BaseService(
            ApiUrls.apiBaseUrl + "Titles"
        );
        titleService.get(this.id).then((data) => {
            if (data.data != null) {
                this.title = data.data;
            }
        });
    }
}
</script>
