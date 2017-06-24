<template>
  <div class="hello">
    <h1>{{ title }}</h1>
    <ul>
      <li v-for="entry in entries" v-bind:key="entry">
        <span>{{ entry.word }}</span>
        <input type="text" v-model="entry.input">
        <span v-show="entry.input === entry.translation" class="mark correct">✓</span>
        <span v-show="entry.input !== entry.translation" class="mark wrong">✗</span>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'hello',
  data () {
    return {
      title: 'Dierennamen',
      entries: []
    }
  },
  created () {
    this.axios.get('http://localhost:5000/api/values').then(response => {
      this.entries = response.data.map(_ => { _.input = ''; return _ })
    })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
ul {
  list-style-type: none;
  padding: 0;
}

.mark.correct {
  color: green;
}

.mark.wrong {
  color: red;
}
</style>
