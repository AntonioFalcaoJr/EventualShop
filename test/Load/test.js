import http from 'k6/http';
export const options = {
  vus: 450,
  duration: '120s',
};
export default function () {
  http.get('http://nginx:5000/api/v1/catalogs');
}