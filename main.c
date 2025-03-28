#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "lista.h"

int main() {
    int opcao;
    char caminho[256];
    printf("Digite o caminho do ficheiro de entrada: ");
    fgets(caminho, sizeof(caminho), stdin);
    caminho[strcspn(caminho, "\n")] = 0;
    CarregarFicheiro(caminho);

    while (1) {
        printf("\nEscolha uma opção:\n");
        printf("1 - Mostrar antenas\n");
        printf("2 - Mostrar efeitos nefastos\n");
        printf("3 - Adicionar antena\n");
        printf("4 - Remover antena\n");
        printf("5 - Sair\n");
        printf("Opção: ");
        scanf("%d", &opcao);
        getchar();
        if (opcao == 1) MostrarAntenas();
        else if (opcao == 2) MostrarEfeitos();
        else if (opcao == 3) {
            char freq;
            int x, y;
            printf("Frequência: ");
            scanf(" %c", &freq);
            printf("Posição X: ");
            scanf("%d", &x);
            printf("Posição Y: ");
            scanf("%d", &y);
            AdicionarAntena(freq, x, y);
            ValidarEfeitos();
        } else if (opcao == 4) {
            int x, y;
            printf("Posição X: ");
            scanf("%d", &x);
            printf("Posição Y: ");
            scanf("%d", &y);
            RemoverAntena(x, y);
            ValidarEfeitos();
        } else if (opcao == 5) {
            break;
        } else {
            printf("Opção inválida!\n");
        }
    }
    return 0;
}
