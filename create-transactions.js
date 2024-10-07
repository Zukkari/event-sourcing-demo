import http from 'k6/http';
import {sleep} from 'k6';

export const options = {
    vus: 1,
    duration: '30s',
};

export default function () {
    const url = "http://localhost:5023/api/v1/transactions";

    const payload = JSON.stringify({
        "from": "74ab6253-ce6d-4359-b12d-17e800c7f2b8",
        "to": "6420d449-d11d-4a1e-a7e0-2e1f96f58d9e",
        "amount": 1
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    http.post(url, payload, params);
}
