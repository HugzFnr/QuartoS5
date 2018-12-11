﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Quarto1
{
    class Program
    {
        static void Main(string[] args)
        {
			/*
            //création d'un chemin d'accès au fichier de sauvegarde qui fonctionne sur tout système
            string fichierActuel = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
            string dossier = Path.GetDirectoryName(fichierActuel);
            string cheminRelatif = @"Sauvegarde.txt";
            string cheminFinal = Path.Combine(dossier, cheminRelatif);
            cheminFinal = Path.GetFullPath(cheminFinal);
			*/

			//LireSauvegarde(positionPiece, contenuCase, cheminFinal);


			//partie test
			//Console.Title{ "Quarto!" }
			//Console.WriteLine("TEST2");
			//string test = "ABCD";
			//Console.WriteLine(test[0]);
			Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            //Console.WriteLine("White on blue.");
            //Console.WriteLine("Another line.");
            //fin de la partie test



            string[] codePiece = new string[]
            {
                "PNRV","PNRE","PNCV","PNCE",
                "PBRV","PBRE","PBCV","PBCE",
                "GNRV","GNRE","GNCV","GNCE",
                "GBRV","GBRE","GBCV","GBCE"
            };

            string[,] symbolePiece = new string[,]
            {
               { "    ", "    ", "    ", "    ", "    ", "    ", "    ", "    ", "(  )","(())","[  ]","[[]]","(  )","(())","[  ]","[[]]" },
               { "(  )", "(())", "[  ]", "[[]]", "(  )", "(())", "[  ]", "[[]]", "(  )","(())","[  ]","[[]]","(  )","(())","[  ]","[[]]" }
            };

			
			//déclaration des tableaux répertoriant la place de chaque pièce et le contenu de chaque case du plateau
			int[] positionPiece;
			int[] contenuCase;
						
			//patie classique
			positionPiece = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }; 
            contenuCase = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };


			/*
			//cases du milieu pour des quarto rapides
			positionPiece = new int[] { -1, -1, -1, -1, 4, 5, 6, -1, 8, 9, 10, -1, -1, -1, -1, -1 };
			contenuCase = new int[] { -1, -1, -1, -1, 4, 5, 6, -1, 8, 9, 10, -1, -1, -1, -1, -1 };

			/*
			AfficherPiecesRestantes(symbolePiece, positionPiece);
			AfficherPlateau(symbolePiece, positionPiece);
			*/

			//affichage des contenus de positionPiece et contenuCase
			//for (int i = 0; i < 16; i++) Console.Write(" {0} ", positionPiece[i]);
			//for (int i = 0; i < 16; i++) Console.Write(" {0} ", contenuCase[i]);

			/*
			//test du programme
			int victoire = -1;
			int[] coup = { -1,-1};
            while (victoire == -1)
            {
				coup = JouerPiece(symbolePiece, codePiece, positionPiece, contenuCase, ia);
				victoire = coup[0];
            }

			AfficherPlateau(symbolePiece, positionPiece);

			Console.WriteLine("\nrangée : {0}\nattribut commun : {1}\n", coup[0], coup[1]);

            Console.WriteLine("\n+------+\n|QUARTO|\n+------+\n");
			*/

			int ia = 0;
			int joueur = 1;

			int premierJoueur = ia;
			DeroulerPartie(symbolePiece, codePiece, positionPiece, contenuCase, premierJoueur);

            Console.ReadLine();
		}

		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
		/// DEROULER PARTIE
		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 

		public static void DeroulerPartie(string[,] symbole, string[] code, int[] position, int[] contenu, int premierJoueur)
		{
			int joueurEnCours = premierJoueur;

			int victoire = -1;
			int[] coup = { -1, -1 };

			while (victoire == -1)
			{
				coup = JouerPiece(symbole, code, position, contenu, joueurEnCours);
				victoire = coup[0];
				joueurEnCours = (joueurEnCours + 1) % 2;
			}

			AfficherPlateau(symbole, position);

			Console.WriteLine("\nrangée : {0}\nattribut commun : {1}\n", coup[0], coup[1]);

			Console.WriteLine("\n+------+\n|QUARTO|\n+------+\n");

            if (joueurEnCours == 0)
                Console.WriteLine("\n+----------------+\n|Victoire de l'IA|\n+----------------+\n");
            else Console.WriteLine("\n+-------------------+\n|Victoire de l'Humain|\n+-------------------+\n");

        }


        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
        /// AFFICHER PLATEAU
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 

        public static void AfficherPlateau(string[,] symbole, int[] position) //affiche le plateau de jeu
        {
            Console.WriteLine("Voici le plateau de jeu :    \n                             ");

            for (int i = 0; i < 4; i++) //4 rangées sur le plateau donc 4 itérations pour mettre en forme
            {
                Console.WriteLine("+----+----+----+----+"); //pour la bordure supérieure de chaque rangée du plateau

                for (int j = 0; j < 2; j++) //il faut afficher chaque pièce sur 2 lignes (partie haute + partie basse)
                {

                    for (int k = 4 * i; k < 4 * (i + 1); k++) //on parcourt le tableau (par rangée, donc 4 par 4) pour placer les pions joués ou numéroter les cases vides
                    {
                        bool piecePlacee = false; //on suppose qu'aucun pion n'a été placé en position k

                        for (int m = 0; m < 16; m++) //on chercher dans le tableau positionPiece[] si il y a une pièce en position k 
                        {
                            if (position[m] == k) //si on trouve la valeur k, on sait que l'indice de la position (cad m) correspond à celui de la pièce correspondante (ça sera symbole[j,m])
                            {
                                //if (3 < k && k < 8 || 11 < k && k < 16) //pour jouer les pièces blanches, on passe momentanément en White pour la couleur du Foreground, puis on repasse en Black
                                if (3 < m && m < 8 || 11 < m && m < 16)
                                {
                                    Console.Write("|");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("{0}", symbole[j, m]);
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else Console.Write("|{0}", symbole[j, m]);
                                piecePlacee = true; //position k n'est pas vide, on met à jour piecePlacee pour passer à la pièce suivante
                            }
                        }

                        if (!piecePlacee) //on numérote la case en prenant en compte l'indentation
                        {
                            if (k + 1 < 10 && j == 0) Console.Write("|{0}   ", k + 1);
                            if (k + 1 >= 10 && j == 0) Console.Write("|{0}  ", k + 1);
                            if (j == 1) Console.Write("|    ", k + 1);
                        }
                    }
                    Console.Write("|\n");
                }
            }
            Console.WriteLine("+----+----+----+----+\n"); //ajout de la bordure inférieure du plateau
        }

        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
        /// AFFICHER PIECES RESTANTES
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 

        public static void AfficherPiecesRestantes(string[,] symbole, int[] position) //affiche les pièces restantes
        {
            Console.WriteLine("Voici les pièces restantes : ");
            for (int i = 0; i < 4; i++) //traitement des 4 lignes de l'affichage
            {
                for (int j = 0; j < 2; j++) //permet de traiter les 2 niveaux de hauteur de chaque pièce dans symbolePiece
                {
                    for (int k = 4 * i; k < 4 * (i + 1); k++)
                    {
                        if (position[k] == -1) //on affiche toute les pièces non placées, dans la bonne couleur
                        {
                            if (3 < k && k < 8 || 11 < k && k < 16)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("{0}   ", symbole[j, k]);
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            else Console.Write("{0}   ", symbole[j, k]);
                        }
                        else Console.Write("       ");
                    }
                    Console.Write(" \n");
                }
                for (int j = 4 * i + 1; j <= 4 * i + 4; j++) //affichage du numéro de chaque case en dessous de celle-ci
                {
                    if (j < 10) Console.Write(" {0}     ", j);
                    else Console.Write(" {0}    ", j);
                    if (j == 8 || j == 12) Console.Write(" \n                            "); //sauter une ligne pour aérer entre les séries de pièces de 2 étages
                }
                Console.Write(" \n");
            }
            Console.WriteLine("                             ");
        }
		
		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
		/// JOUER PIECE
		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 

		public static int[] JouerPiece(string[,] symbole, string[] code, int[] position, int[] contenu, int joueur)
        {
            AfficherPiecesRestantes(symbole, position);
            AfficherPlateau(symbole, position);
			
            int piece;
            if (joueur == 0) //c'est le tour de l'IA, c'est elle qui choisit une piece à jouer
            {
                piece = ChoisirPieceIA(position, contenu, code); //une pièce entre 0 et 15
                Console.WriteLine("L'IA vous donne la piece {0}.", piece + 1);
            }
            else
            {
                piece = ChoisirPieceJoueur(position);
                Console.WriteLine("Vous avez choisi de donner la piece {0} à l'IA.", piece+1);
            }

            /*
            if (joueur == 1) //valide le choix de piece
                piece = ValiderChoix(contenu, position, piece, joueur);
            */

			
            int rangCase;
            if (joueur == 0) //c'est le tour le l'IA, c'est donc au joueur de choisir où jouer la pièce
            {
                rangCase = ChoisirEmplacementJoueur(contenu);
                Console.WriteLine("Vous avez choisi de jouer à l'emplacement {0}.", rangCase + 1);
            }
            else
            {

				//rangCase = ChoisirEmplacementIA(contenu); //une pièce entre 0 et 15

				//nouvelle version de ChoisirEmplacementIA
				rangCase = ChoisirEmplacementIA(position, contenu, code, piece);
				

				Console.WriteLine("L'IA joue à l'emplacement {0}.", rangCase + 1);
            }
            
            /*
            if (joueur == 0) //valide le choix de rangCase
                rangCase = ValiderChoix(contenu, position, rangCase, joueur);
            */

            position[piece] = rangCase;
            contenu[rangCase] = piece;
            
            return (GagnerPartie(position, contenu, code, piece, rangCase));
        }

        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
        /// VALIDER CHOIX
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
		
        public static int ValiderChoix(int[] contenu, int[] position, int choix, int joueur) //indique si le mouvement souhaité est valide ou non.
        //ATTENTION : 'choix' doit correspondre à un INDEX
        {
            if (joueur == 0)
            {
                Console.WriteLine("\nVoulez vous vraiment jouer le pion à l'emplacement {1} ?\n- Entrez o pour valider\n- Entrez n pour saisir à nouveau votre choix", choix);
                string validation = Console.ReadLine();

                while (validation != "o") //tant que le joueur ne valide pas, il faut recommencer
                {
                    Console.WriteLine("\nVous avez choisi d'effectuer un autre mouvement, veuillez préciser votre choix :");
                    Console.WriteLine("- A quel emplacement voulez-vous jouer le pion choisi ?");
                    choix = ChoisirEmplacementJoueur(contenu);

                    Console.WriteLine("\nVoulez vous vraiment jouer le pion à l'emplacement {1} ?\n- Entrez oui pour valider\n- Entrez non pour saisir à nouveau votre choix", choix);
                    validation = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("\nVoulez vous vraiment donner le pion {0} à l'IA ?\n- Entrez o pour valider\n- Entrez n pour saisir à nouveau votre choix", choix);
                string validation = Console.ReadLine();

                while (validation != "o") //tant que le joueur ne valide pas, il faut recommencer
                {
                    Console.WriteLine("\nVous souhaitez choisir une autre pièce, veuillez préciser votre choix :");
                    Console.WriteLine("- Quel pion voulez-vous donner à l'IA ?");
                    choix = ChoisirPieceJoueur(position);

                    Console.WriteLine("\nVoulez vous vraiment donner le pion {0} à l'IA ?\n- Entrez o pour valider\n- Entrez n pour saisir à nouveau votre choix", choix);
                    validation = Console.ReadLine();
                }
            }

            return (choix); //si la piece est disponible et la case libre, le mouvement est valide

        }
		
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
        /// GAGNER PARTIE
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
		
        public static int[] GagnerPartie(int[] position, int[] contenu, string[] code, int piecePlacee, int rangCase)
        //retourne deux nombres, qui indiquent s'il s'agit d'un quarto en ligne ou colonne ou diagonale avec l'index du quarto dans la rangée. A défaut, retourne {-1,-1}
        //on part de l'endroit où la pièce est posée : dans tous les cas, il faut vérifier la colonne et la ligne pour savoir s'il y a un QUARTO
        {
			//par défaut, GagnerPartie renvoie {-1,-1} (il n'y a de quarto dans aucune direction ni aucun caractère commun dans une rangée)
			int[] tabVictoire = { -1, -1 }; //tabVictoire = { direction du quarto, attribut en commun }


			//détermination de la première case chaque rangée
			int ligneCase1 = 4*(rangCase / 4);		//1e case de la ligne, attention puisqu'on veut son indice, IL FAUT BIEN LA MULTIPLIER PAR 4
            int colonneCase1 = rangCase % 4;		//1e case de la colonne
            int diagonaleCase1 = -1;				//1e case de diagonale non définie

			//initialisation de attribut, qui renseigne sur le(s) attributs(s) commun(s) au sein d'une rangée
            int[,] attribut = { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } }; //on suppose que tous les attributs sont identiques au sein d'une rangée
																				  //attribut[0][1] == 1 : attribut 1 commun à la rangée 0
																				  //cad la couleur  est l'attribut commun de la ligne où se trouve la pièce

			//initialisation de rangeeCompletee, qui renseigne sur l'état de chaque rangée
			bool[] rangeeCompletee = { true, true, true };	//on suppose que toutes les rangées sont complètes	
															//rangeeCompletee[0] == 1 : la ligne où se trouve la pièce compte 4 pions


			//détermination des diagonales : si emplacement admet %3 ou %5 = 0.
            //bool pieceSurDiagonale = true;	//on suppose que la piece jouee est sur une diagonale
            if (rangCase % 5 == 0)			// diagonale { 0, 5, 10, 15 } 
				diagonaleCase1 = 0;			//1e case de diagonale
			else if (rangCase % 3 == 0)		//diagonale  { 3, 6, 9, 12 }
				diagonaleCase1 = 3;			//1e case de diagonale
            else rangeeCompletee[2] = false; // la pièce n'est pas sur une diagonale : on procède comme si sa diagonale était incomplète (quarto impossible)


			//initialisation du nombre de directions étudiées (horizontale, verticale, diagonale)
            int nombreDirections;
            if (rangeeCompletee[2]) nombreDirections = 3; //si la pièce est sur une diagonale, on étudie 3 directions
            else nombreDirections = 2;
			

            for (int x = 0; x < nombreDirections; x++) //on définit les directions : 0, 1 et parfois 2 - resp. horizontale, verticale, diagonale. 
            {
				//Pour chaque direction on déclare puis définit les bornes de la recherche et l'incrémentation
				int incrementation;
                int rangMin;
                int rangMax;

                if (x == 0) //cas d'une ligne
                {
                    incrementation = 1;
                    rangMin = ligneCase1;
                    rangMax = ligneCase1 + 3;
                }
                else
                if (x == 1) //cas d'une colonne
                {
                    incrementation = 4;
                    rangMin = colonneCase1;
                    rangMax = colonneCase1 + 3 * 4;
                }
                else //cas d'une diagonale
                {
                    if (diagonaleCase1 == 3)
                    {
                        incrementation = 3;
                        rangMin = diagonaleCase1;
                        rangMax = 12;
                    }
                    else
                    {
                        incrementation = 5;
                        rangMin = diagonaleCase1;
                        rangMax = 15;
                    }
                }

				//on compare les cases au sein d'une rangée 2 à 2 pour trouver les attributs différents
                for (int i = rangMin; i < rangMax; i += incrementation) //on consière les 3 premières cases de la rangée et leurs adjacentes
                {
					//quand on trouve une case vide, on met à jour 'rangeeCompletee' dans la direction (0, 1 ou 2) et attribut
					if (contenu[i] == -1 || contenu[i + incrementation] == -1)
					{
						//DEBOGAGEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
						//Console.WriteLine("suppression de la rangée {0} car la case {1} contient {2} et la case {3} contient {4}", x, i, contenu[i], i + incrementation, contenu[i + incrementation]); 
						rangeeCompletee[x] = false;
						for (int j = 0; j < 4; j++) attribut[x,j] = 0; //aucun caractère commun aux 4 pions de la rangée puisque rangée incomplète
					}

					//si la rangee est complète, on se demande pour chaque attribut s'il est commun entre 2 pièces adjacentes
					if (rangeeCompletee[x])
                        for (int k = 0; k < 4; k++) 
                        {
							//si l'attribut k est distinct entre 2 cases adjacentes, on met à jour attribut[x,k]
							if (code[contenu[i]][k] != code[contenu[i + incrementation]][k]) attribut[x, k] = 0;
                        }
                }
            }

			//on cherche dans attribut s'il y a encore un 1, qui voudrait dire qu'après vérification, il y a bien une similitude entre tous les pions d'une rangée
            for (int x = 0; x < nombreDirections; x++)
            {
				//DEBOGAGEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
				//Console.WriteLine("rangée {0} : a0 = {1} - a1 = {2} - a2 = {3} - a3 = {4}", x, attribut[x,0], attribut[x, 1], attribut[x, 2], attribut[x, 3]);

				for (int i = 0; i < 4; i++)
                {
					//si la case [x,i] est non nulle dans attribut, cela signifie que les cases de la rangée x ont l'attribut i en commun


					if (attribut[x, i] == 1 && rangeeCompletee[x]) 
                    {
                        tabVictoire[0] = x; //on définit la direction du quarto 
                        tabVictoire[1] = i; //on définit l'attribut en commun
                        return tabVictoire; //s'il y a un attribut commun à toutes les pièces d'une rangée, il y a quarto.
                    }
                }
            }

            return (tabVictoire);
        }
		
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
        /// IDENTIFIER CONTENU CASE
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
		
        public static int IdentifierContenuCase(int[] position, int emplacement) //donne le rang de la pièce qui se trouve à 'emplacement' dans 'position[]'
        {
            for (int i = 0; i < 16; i++)
                if (position[i] == emplacement)
                    return (i);
            return (-1);
        }

		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
		/// CHOISIR PIECE IA
		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 

		//fonction qui gère les différentes versions de sélection de pièce IA
		public static int ChoisirPieceIA(int[] position, int[] contenu, string[] code)
		{
			if (ChoisirPieceNonGagnanteIA(position, contenu, code) == -1)
				return ChoisirPieceHasardIA(position);
			else return ChoisirPieceNonGagnanteIA(position, contenu, code);
		}

		//version qui renvoit une pièce prise au hasard	parmi celles disponibles
        public static int ChoisirPieceHasardIA(int[] position) //l'IA choisit aléatoirement une pièce à jouer
        {			
            Random aleatoire = new Random();
            int rangPiece = aleatoire.Next(16); //nb aléatoire entre 0 et 15

            if (position[rangPiece] == -1) //si la piece n'est pas placee, on peut la choisir
                return (rangPiece);
            else //tant que la piece n'est pas valide, on en génère une autre
                while (position[rangPiece] != -1) rangPiece = aleatoire.Next(16);

            return (rangPiece);
        }
		
		//version qui renvoit le rang d'une pièce non gagnante ou -1
		public static int ChoisirPieceNonGagnanteIA(int[] position, int[] contenu, string[] code)
		{
			//on répertorie les pièces disponibles
			int compteur = 0;
			for (int i = 0; i < 16; i++)
				if (position[i] == -1)
					compteur++;

			//création et remplissage d'un tableau qui répertorie les pièces libres
			int[] piecesLibres = new int[compteur];
			int k = 0;
			for (int i = 0; i < 16; i++)
			{
				//on associe à chaque case de piecesLibres le rang de la pièces disponible
				if (position[i] == -1)
				{
					piecesLibres[k] = i;
					k++;
				}
			}

			//on initialise le nombre de coups non gagnants
			int nbCoupsNonGagnants = 0;

			//on change en -1 le rang des pièces qui permettent un coup gagnant et on compte combien il y a de coups non gagnants
			for (int i = 0; i < compteur; i++)
			{
				//si on trouve un coup gagnant avec la pièce considérée, on le signale par un -1
				if (ChoisirEmplacementCoupGagnantIA(position, contenu, code, piecesLibres[i]) != -1)
					piecesLibres[i] = -1;
				else
					nbCoupsNonGagnants++;
			}

			//si tous les coups sont gagnants, on retourne -1
			if (nbCoupsNonGagnants == 0)
				return (-1);
			//sinon, on renvoit une pièce au hasard parmi celles qui ne permettent pas de coup gagnant
			else
			{
				int j = 0;
				//on crée un tableau qui répertorie les coups non gagnants
				int[] coupsNonGagnants = new int[nbCoupsNonGagnants];
				for (int i = 0; i < compteur; i++)
				{
					//si la valeur à l'index i de pièces libres n'est pas -1, c'est qu'on n'a pas trouvé de coup gagnant pour cette pièce, on peut donc la jouer
					if (piecesLibres[i] != -1)
					{
						coupsNonGagnants[j] = piecesLibres[i];
						j++;
					}
				}

				Random aleatoire = new Random();
				int refPieceDonnee = aleatoire.Next(nbCoupsNonGagnants); //nb aléatoire entre 0 et nbCoupsNonGagnants

				//on renvoit le coup non gagnant en position refPieceDonnee de coupsNonGagnants
				return (coupsNonGagnants[refPieceDonnee]);
			}
		}

        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
        /// CHOISIR PIECE JOUEUR
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 

        public static int ChoisirPieceJoueur(int[] position) //le joueur choisit la pièce qu'il veut faire jouer à l'IA
        {
			int rangPiece = DemanderEtConvertirEnNombreEntreeJoueur("Quel pion voulez-vous faire jouer à l'IA ?") - 1;

            while (rangPiece < 0 || rangPiece > 15)
            {
                Console.WriteLine("\nLes données indiquées ne correspondent pas à un coup valide !\n");
                rangPiece = DemanderEtConvertirEnNombreEntreeJoueur("Veuillez saisir à nouveau la pièce à jouer :") - 1;
            }

            if (position[rangPiece] == -1) //si la piece n'est pas placee, on peut la choisir
                return (rangPiece);

            else //tant que la piece n'est pas valide, on demande une autre pièce à faire jouer à l'IA
                while (position[rangPiece] != -1)
                {
                    Console.WriteLine("Cette pièce est déjà jouée, veuillez en sélectionner une autre.");
                    rangPiece = DemanderEtConvertirEnNombreEntreeJoueur("Quel pion voulez-vous faire jouer ?") - 1;

					while (rangPiece - 1 < 0 || rangPiece - 1 > 15)
                    {
                        Console.WriteLine("\nLes données indiquées ne correspondent pas à un coup valide !");
                        rangPiece = DemanderEtConvertirEnNombreEntreeJoueur("Veuillez saisir à nouveau la pièce à jouer :") - 1;
					}

				}

            return (rangPiece);
        }

		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
		/// CHOISIR EMPLACEMENT IA
		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 

		//fonction qui gère les différentes versions de sélection de case IA
		public static int ChoisirEmplacementIA(int[] position, int[] contenu, string[] code, int pieceDonnee)
		{
			if (ChoisirEmplacementCoupGagnantIA(position, contenu, code, pieceDonnee) == -1)
				return ChoisirEmplacementHasardIA(contenu);
			else return ChoisirEmplacementCoupGagnantIA(position, contenu, code, pieceDonnee);
		}

		//version qui joue au hasard		
		public static int ChoisirEmplacementHasardIA(int[] contenu) //l'IA choisit aléatoirement une case pour jouer sa pièce
        {			
            Random aleatoire = new Random();
            int rangCase = aleatoire.Next(16); //nb aléatoire entre 0 et 15

            if (contenu[rangCase] == -1) //si l'emplacement est libre, on peut le choisir
                return (rangCase);
            else //tant que l'emplacement n'est pas valide, on en génère une autre
                while (contenu[rangCase] == -1) rangCase = aleatoire.Next(16);

            return (rangCase);			
		}
		

		public static int ChoisirEmplacementCoupGagnantIA(int[] position, int[] contenu, string[] code, int pieceDonnee)
		{	//IA qui repère si elle a un coup gagnant avec la pieceDonnee

			//décompte du nombre de cases libres sur le plateau
			int compteur = 0;
			for (int i = 0; i < 16; i++)
			{
				if (contenu[i] == -1)
					compteur++;
			}

			//création et remplissage d'un tableau qui répertorie les cases libres du plateau
			int[] positionsLibres = new int[compteur];
			int k = 0;
			for (int i = 0; i < 16; i++)
			{
				if (contenu[i] == -1)
				{
					//on répertorie la position libre
					positionsLibres[k] = i;
					k++;
				}
			}

			int iterations = 0;
			int[] copieContenu = new int[16];
			
			while(iterations < compteur)
			{
				//on crée une copie de contenu
				for (int i = 0; i < 16; i++)
				{
					copieContenu[i] = contenu[i];
				}

				copieContenu[positionsLibres[iterations]] = pieceDonnee;
				//on vérifie s'il y a Quarto en posant la piece à la position libre			
				if (GagnerPartie(position, copieContenu, code, pieceDonnee, positionsLibres[iterations])[0] != -1)
					//si on trouve un quarto on renvoit la position gagnante
					return (positionsLibres[iterations]);

				iterations++;
			}
			
			//si on ne trouve pas de Quarto, on renvoit -1
			return (-1);
		}


		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
		/// CHOISIR EMPLACEMENT JOUEUR
		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 

		public static int ChoisirEmplacementJoueur(int[] contenu) //le joueur choisit une case pour jouer sa pièce
        {
			int rangCase = DemanderEtConvertirEnNombreEntreeJoueur("Où voulez-vous placer la pièce ?") -1;

            while (rangCase < 0 || rangCase > 15)
            {
                Console.WriteLine("\nLes données indiquées ne correspondent pas à un coup valide !\nVeuillez saisir à nouveau où placer la pièce :");
                rangCase = DemanderEtConvertirEnNombreEntreeJoueur("Où voulez-vous placer la pièce ?") - 1;
            }

            if (contenu[rangCase] == -1) //si l'emplacement est libre, on peut le choisir
                return (rangCase);

            else //tant que l'emplacement n'est pas valide, on demande une autre case où jouer la pièce
                while (contenu[rangCase] != -1)
                {
                    Console.WriteLine("Cette case est déjà occupée, veuillez en sélectionner une autre.");
                    rangCase = DemanderEtConvertirEnNombreEntreeJoueur("Où voulez-vous placer la pièce ?") - 1;

                    while (rangCase - 1 < 0 || rangCase - 1 > 15)
                    {
                        Console.WriteLine("\nLes données indiquées ne correspondent pas à un coup valide !\nVeuillez saisir à nouveau la pièce à jouer :");
                        rangCase = DemanderEtConvertirEnNombreEntreeJoueur("Où voulez-vous placer la pièce ?") - 1;
                    }
                }

            return (rangCase);
        }


		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
		/// CONVERTIR POSITION CONTENU
		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///         

		public static int[] ConvertirPositionContenu(int[] position)
        {
            int[] contenu = new int[16];
            for (int i = 0; i < 16; i++)
            {
                contenu[i] = -1;
                for (int k = 0; k < 16; k++)
                {
                    if (position[k] == i) contenu[i] = position[k];
                }
            }

            for (int i = 0; i < 16; i++)
            {
                Console.Write(" " + contenu[i]);
            }
            return (contenu);
        }


		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
		/// DEMANDER ET CONVERTIR EN NOMBRE ENTREE JOUEUR
		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///         

		public static int DemanderEtConvertirEnNombreEntreeJoueur(string consigne)
		{
			//initialisation du nombre à renvoyer
			int nombre = 0;

			//applique la consigne et enregistre la réponse du joueur
			Console.WriteLine(consigne);
			string reponseJoueur = Console.ReadLine();

			//teste si l'entrée du joueur est bien un nombre
			bool convertible = int.TryParse(reponseJoueur, out nombre);

			//tant que l'entrée n'est pas un nombre, on redemande au joueur de saisir un nombre
			while (!convertible)
			{
				Console.WriteLine("Votre entrée n'est pas un entier, veuillez saisir une donnée correcte.");
				Console.WriteLine(consigne);
				reponseJoueur = Console.ReadLine();
				convertible = int.TryParse(reponseJoueur, out nombre);
			}
			nombre = int.Parse(reponseJoueur);			
			return (nombre);
		}


		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
		/// /// SAUVEGARDER PARTIE
		/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///         


		public static void SauvegarderPartie(int[] contenu, string[] pieces, string chemin)
		{
			string[] lignes = new string[16];

			for (int iter = 0; iter <= 15; iter++)
			{
                int k = contenu[iter];
                lignes[iter] = k.ToString();

            }
            File.WriteAllLines(chemin, lignes); //TO DO pour la sauvegarde : fonction lireSauvegarde et intégrer l'option à la boucle de jeu
        }


        public static bool LireSauvegarde(int[] position, int[] contenu, string chemin)
        {
            string ligne;
            bool sauvegardeValide = true;
            int iter = 0;
            StreamReader fichier = new StreamReader(chemin);
            while ((sauvegardeValide) && (iter < 16) && ((ligne = fichier.ReadLine()) != null))
            {
                //gérer les cases vides
                position[int.Parse(ligne)] = iter;
                contenu[iter] = int.Parse(ligne);
            }
            fichier.Close();

            if (iter == 15)
                return sauvegardeValide = true;
            else
                return sauvegardeValide = false;
        }
    }
}

