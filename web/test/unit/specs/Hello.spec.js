import Vue from 'vue'
import Hello from '@/components/Hello'

describe('Hello.vue', () => {
  it('should render correct contents', () => {
    const Constructor = Vue.extend(Hello)
    const vm = new Constructor().$mount()
    expect(vm.$el.querySelector('.hello h1').textContent)
      .to.equal('Dierennamen')
  })

  it('should render the entries', (done) => {
    const Constructor = Vue.extend(Hello)
    const vm = new Constructor().$mount()
    vm.$data.entries = [{
      id: 1, word: 'aap', 'translation': 'monkey'
    }]

    vm.$nextTick(() => {
      expect(vm.$el.querySelector('.hello ul li span:first-child').textContent)
        .to.equal('aap')

      done()
    })
  })
})
