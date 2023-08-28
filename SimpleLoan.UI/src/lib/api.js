const apiBaseUrl = 'https://localhost:7078';

/**
 * Get available loan types
 */
export function getLoanTypes() {
    return get(apiBaseUrl + '/loan/types');
}

/**
 * Get available loan types
 * @param {string} type desired loan type
 * @param {Number} amount loan amount
 * @param {Number} period loan period (in months)
 */
export function getCalculation(type, amount, period) {
    return get(apiBaseUrl + `/plan?type=${type}&period=${period}&amount=${amount}`);
}

function get(url) {
    return new Promise((resolve, reject) => {
        fetch(url, { method: 'GET' })
        .then(r => {
            if (!r.ok) {
                console.log(r);
                throw new Error(r.statusText);
            }
            return r.json();
        })
        .then(resolve)
        .catch(reject);
    });
}