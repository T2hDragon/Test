<template>
    <div class="about">
        <h1>This is an about page {{id}}</h1>
    </div>
</template>
<script lang="ts">
import { Options, Vue } from "vue-class-component";

@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class Home extends Vue {
    // id!: string;
}
</script>
