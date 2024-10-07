import http from 'k6/http';
import {sleep} from 'k6';

export const options = {
    vus: 1,
    duration: '5s',
};

export default function () {
    const url = "http://localhost:5023/api/v1/transactions";

    const payload = JSON.stringify({
        "from": "5e95eaa1-655e-4506-abb5-0c3fad384c4b",
        "to": "129f7221-cb53-49fd-aeba-1fdba232d3bb",
        "amount": 1
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    http.post(url, payload, params);
}
