<script>
	import PaymentPlan from '$lib/PaymentPlan.svelte';
import { getLoanTypes, getCalculation } from '$lib/api';
	import { onMount } from 'svelte';

	let loanAmount = 100000;
	let months = 12;
	let loanTypes = [''];
	let type = '';
	let isLoading = false;
	let isError = false;
	let payments = [];

	onMount(async () => {
		loanTypes = await getLoanTypes();
		type = loanTypes.length > 0 ? loanTypes[0] : '';
	});

	async function onCalculate() {
		isLoading = true;
		try {
			payments = await getCalculation(type, loanAmount, months);
			isError = false;
		} catch (e) {
			isError = true;
			payments = [];
		}
		isLoading = false;
	}

	async function onReset() {
		isLoading = false;
		payments = [];
	}
</script>

<section class="section is-medium">
	<h1 class="title">Simple Loan</h1>
	<h2 class="subtitle">A simple loan calculator that can calcuate your payment schedule.</h2>

	<div class="field">
		<div class="field-body">
			<div class="field is-narrow">
				<div class="control">
					<label class="label" for="loan">Amount</label>
					<div class="control">
						<input 
							id="loan"
							class="input"
							type="number"
							min="1000"
							max="999999999"
							bind:value={loanAmount}
						/>
					</div>
				</div>
			</div>

			<div class="field is-narrow">
				<div class="control">
					<label class="label" for="months">Months</label>
					<div class="control">
						<input id="loan" class="input" type="number" min="12" max="360" bind:value={months} />
					</div>
				</div>
			</div>
		</div>
	</div>

	<div class="field">
		<label class="label" for="type">Loan Type</label>
		<div class="control">
			<div class="select">
				<select bind:value={type}>
					<option value="none" selected disabled hidden>...</option>
					{#each loanTypes as item}
						<option value={item}>{item}</option>
					{/each}
				</select>
			</div>
		</div>
	</div>

	<div class="field is-grouped">
		<p class="control">
			<button class="button is-primary" on:click={onCalculate}>Calculate</button>
		</p>
		<p class="control">
			<button class="button is-light" on:click={onReset}>Reset</button>
		</p>
	</div>

	<PaymentPlan payments={payments} />
</section>
