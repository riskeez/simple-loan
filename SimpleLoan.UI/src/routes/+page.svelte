<script>
	import { getLoanTypes, getCalculation } from '$lib/api';
	import { onMount } from 'svelte';

	let loanAmount = 100000;
	let months = 12;
	let loanTypes = [''];
	let type = '';
    let isLoading = false;
    let payments = [];

	onMount(async () => {
		loanTypes = await getLoanTypes();
		type = loanTypes.length > 0 ? loanTypes[0] : '';
	});

	async function calculate() {
        payments = await getCalculation(type, loanAmount, months);
        console.log(payments);
	}
</script>

<div>
	<label for="loan">Amount</label>
	<input id="loan" type="number" bind:value={loanAmount} />
</div>

<div>
	<label for="months">Months</label>
	<input id="months" type="number" bind:value={months} />
</div>

<div>
	<label for="type">Type</label>
	<select bind:value={type}>
		{#each loanTypes as item}
			<option value={item}>{item}</option>
		{/each}
	</select>
</div>

<div>
	<button on:click={calculate}> Calculate</button>
</div>
