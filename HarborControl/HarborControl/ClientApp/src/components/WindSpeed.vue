<template>
    <div>
        <a class="navbar-brand">Wind Speed: {{windSpeed}} km/h</a>
    </div>
</template>


<script>
    import axios from 'axios'
    export default {
        name: "WindSpeed",
        data() {
            return {
                windSpeed: 0
            }
        },
        methods: {
            getWindSpeed() {
                axios.get('api/weather')
                    .then((response) => {
                        this.windSpeed = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getWindSpeed();
            setInterval(this.getWindSpeed, 10 * 1000);
        }
    }
</script>