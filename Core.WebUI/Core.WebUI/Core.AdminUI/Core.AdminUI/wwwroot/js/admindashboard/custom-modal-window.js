function runModalWindow(options) {
    const modal = document.getElementById(options.modalElementId);

    if (!modal) return;

    function openDeleteModal(releatedBtnData) {

        options["inputElementIdList"].forEach((inputItem) => {
            document.getElementById(inputItem).textContent = releatedBtnData[inputItem] || "-";
        });

        modal.classList.add('is-open');
    }

    function closeDeleteModal() {
        modal.classList.remove('is-open');
    }

    // Tablodaki tüm silme butonlarını dinle (Dinamik/sayfalı tablolar için event delegation)
    openDeleteModal(options.releatedBtnData);

    // Vazgeç, ESC etiketi veya arka plana tıklayınca kapat
    document.getElementById(options.cancelBtnId).addEventListener('click', closeDeleteModal);
    modal.addEventListener('click', function (e) {
        if (e.target === modal) closeDeleteModal();
    });

    // Klavyeden ESC tuşuna basınca kapat
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && modal.classList.contains('is-open')) {
            closeDeleteModal();
        }
    });
}