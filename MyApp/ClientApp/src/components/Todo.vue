<template>
    <div class="container">
        <div class="row">
            <div class="col-12">
                <TodoHeader />
            </div>
        </div>
        <div class="row mt-5 justify-content-between">
            <div class="col-md-5 mt-3">
                <TodoInput />
            </div>
            <div class="col-md-6">
                <TodoTable />
            </div>
        </div>
        <div class="row mt-5 justify-content-center" v-if="loading">
            <div class="col-1">
                <div class="spinner-border" style="margin: 0 auto" role="status"></div>
            </div>
        </div>
    </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';
import { mapState } from "vuex";
import TodoTable from "./Todo/Table.vue";
import TodoInput from "./Todo/Input.vue";
import TodoHeader from "./Todo/Header.vue";

export default defineComponent({
    components: {
        TodoTable,
        TodoInput,
        TodoHeader
    },
    computed: mapState(['loading']),
    async created() {
        this.$store.dispatch('getTodos');
    }
});
</script>