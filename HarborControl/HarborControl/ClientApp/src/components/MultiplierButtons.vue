<template>
    <span>{{time}}</span>
    <div class="btn-group btn-group-toggle" data-toggle="buttons">
        <label class="btn btn-secondary" v-bind:class="{ active: is1XActive }">
            <input type="radio" name="options" id="1x" (click)="selectMultiplier(1)"> 1x
        </label>
        <label class="btn btn-secondary">
            <input type="radio" name="options" id="2x" (click)="selectMultiplier(2)"> 2x
        </label>
        <label class="btn btn-secondary">
            <input type="radio" name="options" id="4x" (click)="selectMultiplier(4)"> 4x
        </label>
        <label class="btn btn-secondary">
            <input type="radio" name="options" id="8x" (click)="selectMultiplier(8)"> 8x
        </label>
        <label class="btn btn-secondary">
            <input type="radio" name="options" id="16x" (click)="selectMultiplier(16)"> 16x
        </label>
    </div>
</template>


<script>
    import axios from 'axios'
    export default {
        name: "MultiplierButtons",
        data() {
            return {
                is1XActive: true,
                is2XActive: false,
                is4XActive: false,
                is8XActive: false,
                is16XActive: false,
                time: ""
            }
        },
        methods: {
            getMultiplierAndTime() {
                axios.get('api/clock')
                    .then((response) => {
                        this.is1XActive = response.data.multiplier === 1;
                        this.is2XActive = response.data.multiplier === 2;
                        this.is4XActive = response.data.multiplier === 4;
                        this.is8XActive = response.data.multiplier === 8;
                        this.is16XActive = response.data.multiplier === 16;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            selectMultiplier(multiplier) {
                axios.post(`api/clock?multiplier=${multiplier}`)
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getMultiplierAndTime();
            setInterval(this.getMultiplierAndTime, 10 * 1000);
        }
    }
</script>

<style>
    a.container {
        display: flex;
        justify-content: space-evenly;
    }
</style>