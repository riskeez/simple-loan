<script>
	export let payment = {};
	let isHidden = true;
	function onHeaderClick() {
		isHidden = !isHidden;
	}
</script>

<div class="card">
	<header class="card-header" on:click={onHeaderClick}>
		<p class="card-header-title">{payment.periodNo} | To Pay: {payment.toPay}</p>
		<button class="card-header-icon" aria-label="more options">
			<span class="icon">
				<i class="fas fa-angle-down" aria-hidden="true" />
			</span>
		</button>
	</header>
	<div class="card-content" class:is-hidden={isHidden}>
		<div class="content">
			<div class="columns">
				<div class="column is-narrow "><b>Interest:</b> {payment.interestPayment}</div>
				<div class="column is-narrow "><b>Principal Payment:</b> {payment.principalPayment}</div>
				<div class="column is-narrow "><b>Remaining:</b> {payment.remainingLoan}</div>
			</div>
		</div>
	</div>
</div>

<style>
	.card {
		margin-top: 20px;
	}
</style>
