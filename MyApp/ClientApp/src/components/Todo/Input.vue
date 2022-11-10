<template>
    <div>
        <form @submit.prevent="onSubmit">
            <div>
                <div class="row">
                    <div class="col-sm-12">
                        <label for="Todo"><b>Todo</b></label>
                        <input class="form-control" name="todo" v-model="todo" />
                        <div id="todo-validation" class="invalid-feedback">
                            {{ this.todoError }}
                        </div>
                    </div>
                </div>
                <div class="row justify-content-between mb-3">
                    <div class="col-sm-6 mt-1">
                        <label for="Date"><b>Date</b></label>
                        <Datepicker name="datepicker" v-model="date"></Datepicker>
                        <div id="date-validation" class="invalid-feedback">
                            {{ this.dateError }}
                        </div>
                    </div>
                    <div class="col-auto">
                        <button type="submit" class="btn btn-primary mt-4">
                            Post todo
                        </button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12"><b>Test settings</b></div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <div class="form-check mt-3 form-switch">
                            <input class="form-check-input" @click="toggleBackend" v-model="backendEnabled"
                                type="checkbox" role="switch" id="switchBackendApi" />
                            <label class="form-check-label" for="switchBackendApi">Enable backend API</label>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="form-check mt-2 form-switch">
                            <input class="form-check-input" @click="toggleValidations" v-model="validationsEnabled"
                                type="checkbox" role="switch" id="switchInputValidation" />
                            <label class="form-check-label" for="switchInputValidation">Enable frontent input
                                validation</label>
                        </div>
                    </div>
                </div>
            </div>
        </Form>
    </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'

export default defineComponent({
    components: {
        Datepicker
    },
    data() {
        return {
            date: null,
            todo: "",
            backendEnabled: true,
            validationsEnabled: true,
            dateError: "",
            todoError: ""
        };
    },
    methods: {
        toggleBackend() {
            this.$store.dispatch('toggleBackend', !this.backendEnabled)
        },
        toggleValidations() {
            this.dateError = ""
            this.todoError = ""
        },
        isValid() {
            var valid = true
            if (this.validationsEnabled) {
                if (this.todo == null || this.todo.length < 5 || this.todo.length > 30) {
                    this.todoError = "Message length between 5 and 30 characters"
                    valid = false
                } else {
                    this.todoError = ""
                }
                if (this.date == null) {
                    this.dateError = "Choose a date"
                    valid = false
                } else {
                    this.dateError = ""
                }
            }
            return valid
        },
        async onSubmit() {
            if (this.isValid()) {
                var that = this
                this.$store.dispatch('postTodo', { date: this.date, todo: this.todo }).then(() => {
                    this.$nextTick(() => {
                        that.date = null;
                        that.todo = ""
                    });
                })
            }
        }
    }
});
</script>

<style>
.invalid-feedback {
    display: block;
}
</style>