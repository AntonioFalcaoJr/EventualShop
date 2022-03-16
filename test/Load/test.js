import http from 'k6/http';
export const options = {
  vus: 100,
  duration: '60s',
};
export default function () {
  http.get('http://nginx:4000/api/v1/shopping-carts/81f21228-4450-44bc-9ca9-47e13f8f14aa');
}