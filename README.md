# Fallout New Vegas Trainer

  Feito apenas com o propósito de alterar valores de memória volátil de determinados processos, nesse caso, do Fallout New Vegas. O código consiste em capturar com base no nome do processo utilizando da biblioteca do kernel32.dll, não se limitando ao acesso, como modificação de valores. No modelo inicial, fazemos alteração da quantidades de Caps (moeda do jogo) enquanto o mesmo aberto.

## Ferramentas e bibliotecas utilizadas
- Visual Studio
- Cheat Engine

  Todo o processo de captura foi feito utilizando System.Diagnostics e o System.Runtime.InteropServices, mas não se limitando apenas a isso, já que foi necessário também fazer uso da ferramenta Cheat Engine para capturar valores e filtrar a fim de encontrar o endereço de memória correspondente a quantidade de Caps que o jogador possui.
