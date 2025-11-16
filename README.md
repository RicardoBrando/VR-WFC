## **Résultats / captures :**

**Génération complète**
![Generation complete](https://github.com/RicardoBrando/VR-WFC/blob/main/screenshots/generation_complete.png)

**Bouton de génération**
![Bouton de generation](https://github.com/RicardoBrando/VR-WFC/blob/main/screenshots/button_generation.png)

**Visuel lors de l'interaction avec la mini map**
![Minimap hover](https://github.com/RicardoBrando/VR-WFC/blob/main/screenshots/minimap_hover.png)

**Visuel lors de la sélection d'une tuile**
![Minimap selection](https://github.com/RicardoBrando/VR-WFC/blob/main/screenshots/minimap_selection.png)

**Résultat de génération partielle à partir de la première génération**
![2nd generation](https://github.com/RicardoBrando/VR-WFC/blob/main/screenshots/minimap_generation_2.png)

# Démo

### Informations sur le fonctionnement de l'environnement

- Navigation : le bouton "Switch Mode" du controller gauche permet de passer d'un mode de déplacement à un autre  
  -> Déplacement continu avec le stick gauche  
  -> Téléportation (stick gauche vers l'avant) / retour à la dernière position (stick gauche vers l'arrière). Des "fantômes" sont placés au différents points de départ de chaque téléportation  

- Interaction : le bouton "Select" du controller droit permet l'interaction avec divers éléments de l'environnement  
  -> Bouton de génération de l'environnement  
  -> Carte interactive (sélection des tuiles à supprimer lors de la nouvelle génération)  

- Génération : il arrive que l'algorithme ne donne aucune solution (même lors de la première génération), surtout lorsqu'on garde un grand nombre de tuiles de la dernière génération  
  -> il faut un échantillon de départ très complet afin que l'algorithme trouve un maximum de possibilités, aussi il faut paramétrer le fichier XML pour influer sur la génération  

**Vidéo**
[![Demo](https://github.com/RicardoBrando/VR-WFC/blob/main/screenshots/button_generation.png)](https://github.com/RicardoBrando/VR-WFC/blob/main/screenshots/demo.mp4)
