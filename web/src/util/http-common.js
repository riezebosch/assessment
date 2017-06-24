import axios from 'axios'

export const HTTP = axios.create({
  baseURL: process.env.NODE_ENV === 'production' ? '/api/' : 'http://localhost:5000/api/'
})
