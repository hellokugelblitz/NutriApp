<script lang="ts">
    export let showModal = false;
    export let title = "";
    export let content = "";
    export let onClose = () => {};

    function handleClose() {
        onClose();
    }
</script>

{#if showModal}
    <div class="modal-overlay">
        <div class="modal">
            <div class="modal-header">
                <h2>{title}</h2>
                <button on:click={handleClose}>&#10006;</button>
            </div>
            <div class="modal-content">
                {@html content}
            </div>
        </div>
    </div>
{/if}

<style>
    .modal-overlay {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.6);
        display: flex;
        justify-content: center;
        align-items: center;
        z-index: 1000;
    }

    .modal {
        background-color: white;
        padding: 20px;
        border-radius: 5px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        width: 50%;
        max-width: 500px;
        z-index: 1001;
    }

    .modal-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 20px;
    }

    .modal-header h2 {
        margin: 0;
    }

    .modal-header button {
        cursor: pointer;
        border: none;
        background: none;
        font-size: 1.2rem;
    }

    .modal-content {
        line-height: 1.6;
        font-size: 1rem;
    }
</style>
