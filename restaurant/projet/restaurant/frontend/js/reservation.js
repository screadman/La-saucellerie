function ajouterProduit(id, nom, prix) {
    const quantite = document.getElementById(`quantity${id}`).value;
    sessionStorage.setItem(`quantite${id}`, quantite);
    sessionStorage.setItem(`id${id}`, id);
    sessionStorage.setItem(`nom${id}`, nom);
    sessionStorage.setItem(`prix${id}`, Number(quantite) * prix);
}

function afficherReservation() {
    let total = 0;

    for (let indice = 1; indice <= 6; indice++) {
        const quantite = parseInt(sessionStorage.getItem(`quantite${indice}`), 10);
        const prixUnitaire = parseFloat(sessionStorage.getItem(`prix${indice}`));

        if (quantite > 0 && !isNaN(prixUnitaire)) {
            total += prixUnitaire;
            facture(indice);
        }
    }

    document.getElementById("prixTotal").innerHTML = `${total} $`;
    const reduction = (total * 10) / 100;
    document.getElementById("reduction").innerHTML = `${reduction} $`;
    document.getElementById("totalPayer").innerHTML = `${total - reduction} $`;
}

function facture(identifiant) {
    const grillePanier = document.createElement("div");
    grillePanier.classList.add("row", "border-bottom", "py-2", "mb-2");

    const nomProduit = sessionStorage.getItem(`nom${identifiant}`);
    const quantite = sessionStorage.getItem(`quantite${identifiant}`);
    const prix = sessionStorage.getItem(`prix${identifiant}`);

    grillePanier.innerHTML = `
        <div class="col-6">
            <h6>${nomProduit}</h6>
            <p class="mb-0 text-muted">Qté: ${quantite}</p>
        </div>
        <div class="col-3">
            <p class="text-end">${prix} $</p>
        </div>
        <div class="col-3 text-end">
            <button class="btn btn-danger btn-sm" onclick="supprimerElement(${identifiant})">
                <i class="fa-solid fa-xmark"></i>
            </button>
        </div>
    `;

    document.getElementById("panier").appendChild(grillePanier);

    document.getElementById("titrePanierVide").classList.add("d-none");
    document.getElementById("boutonEffacer").classList.remove("d-none");
    document.getElementById("total").classList.remove("d-none");
}

function supprimerElement(produit) {
    sessionStorage.removeItem(`quantite${produit}`);
    sessionStorage.removeItem(`nom${produit}`);
    sessionStorage.removeItem(`prix${produit}`);
    location.reload();
}

function viderPanier() {
    sessionStorage.clear();
    location.reload();
}

document.getElementById("imprimer").addEventListener("click", () => {
    const contenu = document.getElementById("contenu");
    if (contenu) {
        window.print();
    } else {
        alert("Aucun contenu à imprimer !");
    }
});