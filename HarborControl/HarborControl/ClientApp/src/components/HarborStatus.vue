<template>
    <h1 id="tableLabel">Harbor Control Traffic Status</h1>

    <p v-if="!isLoaded"><em>Loading...</em></p>

    <a class="container" v-if="isLoaded">
        <table class='table' aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Perimeter</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="perimeterVessel of vesselsInPerimeter" v-bind:key="perimeterVessel.name">
                    <td>{{ perimeterVessel.name }}</td>
                </tr>
            </tbody>
        </table>
        <table class='table' aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>In Tranist</th>
                </tr>
            </thead>
            <tbody>
                <tr v-if="vesselInTransit">
                    <td>{{ vesselInTransit.name }}</td>
                </tr>
            </tbody>
        </table>
        <table class='table' aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Harbor</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="harborVessel of vesselsInHarbor" v-bind:key="harborVessel.name">
                    <td>{{ harborVessel.name }}</td>
                </tr>
            </tbody>
        </table>
    </a>
</template>


<script>
    import axios from 'axios'
    export default {
        name: "HarborStatus",
        data() {
            return {
                vesselsInPerimeter: [],
                vesselsInHarbor: [],
                vesselInTransit: {},
                isLoaded: false
            }
        },
        methods: {
            getTraffic() {
                axios.get('api/traffic')
                    .then((response) => {
                        this.vesselsInPerimeter = response.data.perimeter;
                        this.vesselsInHarbor = response.data.harbor;
                        this.vesselInTransit = response.data.transit;
                        this.isLoaded = true;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getTraffic();
            setInterval(this.getTraffic, 10*1000);
        }
    }
</script>

<style>
    a.container {
        display: flex;
        justify-content: space-evenly;
    }
</style>