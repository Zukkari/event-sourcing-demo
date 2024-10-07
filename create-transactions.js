import http from 'k6/http';
import {sleep} from 'k6';

export const options = {
    vus: 1,
    duration: '5s',
};

export default function () {
    const url = "http://localhost:5023/api/v1/transactions";

    const payload = JSON.stringify({
        "from": "5b0774a2-8cd4-488c-b525-bb81384568f0",
        "to": "b408b513-f7c1-46dd-90cc-b6f2fedf1995",
        "amount": 1
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    http.post(url, payload, params);
}
