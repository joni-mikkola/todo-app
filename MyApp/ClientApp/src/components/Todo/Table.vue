<template>
    <table class="table table-hover table-striped">
        <thead>
            <tr>
                <th width="40">Done</th>
                <th width="150">Date</th>
                <th>Message</th>
                <th>Delete</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="todo in todos" v-bind:key="todo.id">
                <th><span style="cursor: pointer;" @click="toggle(todo)" class="material-icons-outlined">{{
                        formatDone(todo.done)
                }}</span>
                </th>
                <th>{{ formatDate(todo.date) }}</th>
                <th>{{ todo.message }}</th>
                <th><span style="cursor: pointer;" @click="remove(todo)"
                        class="red material-icons-outlined">remove</span></th>
            </tr>
        </tbody>
    </table>
</template>
  
<script lang="js">
import { defineComponent } from 'vue';
import { mapState } from "vuex";
import moment from 'moment'

export default defineComponent({
    components: {},
    computed: mapState(['todos']),
    methods: {
        toggle(todo) {
            var index = this.findIndex(todo.id)
            this.$store.dispatch('toggleTodo', todo.id).then((response) => {
                this.todos[index] = response.data
            })
        },
        remove(todo) {
            var index = this.findIndex(todo.id)
            this.$store.dispatch('deleteTodo', todo.id).then(() => {
                this.todos.splice(index, 1)
            });
        },
        findIndex(id) {
            return this.todos.findIndex(x => x.id == id);
        },
        formatDate(str) {
            var date = moment(str);
            return date.utc().format('HH:mm DD.MM.YYYY');
        },
        formatDone(done) {
            if (done == null) {
                return 'check_box_outline_blank'
            }
            return 'check_box'
        }
    }
});
</script>
<style>
.red {
    color: red
}
</style>