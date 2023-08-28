<script>
	import PaymentPeriod from "./PaymentPeriod.svelte";

	export let payments = [];
    let expand = false;
</script>

{#if payments && payments.length > 0}
    {#each payments as payment}
        <PaymentPeriod payment={payment} />
    {/each}
{/if}