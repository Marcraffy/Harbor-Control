<template>
    <div>
        <span>{{time}}</span>
    </div>
    <div class="btn-group btn-group-toggle" data-toggle="buttons">
        <label class="btn btn-secondary" v-bind:class="{ active: is1XActive }">
            <input type="radio" name="options" id="1x" v-on:click="selectMultiplier(1)"> 1x
        </label>
        <label class="btn btn-secondary" v-bind:class="{ active: is2XActive }">
            <input type="radio" name="options" id="2x" v-on:click="selectMultiplier(2)"> 2x
        </label>
        <label class="btn btn-secondary" v-bind:class="{ active: is4XActive }">
            <input type="radio" name="options" id="4x" v-on:click="selectMultiplier(4)"> 4x
        </label>
        <label class="btn btn-secondary" v-bind:class="{ active: is8XActive }">
            <input type="radio" name="options" id="8x" v-on:click="selectMultiplier(8)"> 8x
        </label>
        <label class="btn btn-secondary" v-bind:class="{ active: is16XActive }">
            <input type="radio" name="options" id="16x" v-on:click="selectMultiplier(16)"> 16x
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
                        this.setActiveButton(response.data.multiplier);
                        this.time = response.data.currentTime;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            selectMultiplier(multiplier) {
                axios.post(`api/clock?multiplier=${multiplier}`)
                    .then(() => {
                        this.setActiveButton(multiplier);
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            setActiveButton(multiplier) {
                this.is1XActive = multiplier === 1;
                this.is2XActive = multiplier === 2;
                this.is4XActive = multiplier === 4;
                this.is8XActive = multiplier === 8;
                this.is16XActive = multiplier === 16;
            }
        },
        mounted() {
            this.getMultiplierAndTime();
            setInterval(this.getMultiplierAndTime, 10 * 1000);
        }
    }
</script>