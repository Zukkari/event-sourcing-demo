import http from 'k6/http';
import {sleep} from 'k6';

export const options = {
    vus: 1,
    duration: '5s',
};

export default function () {
    const url = "http://localhost:5023/api/v1/transactions";

    const payload = JSON.stringify({
        "from": "9501d105-72f6-4139-8824-f1422dfa410d",
        "to": "38910c99-6e34-495e-8be7-b8388e9049d3",
        "amount": 1
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    http.post(url, payload, params);
}
