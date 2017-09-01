public class MinoShape {
	public int[,] cell = new int[5,5];

	public MinoShape(){
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				cell[j, i] = 0;
			}
		}
	}

	public void SetCell(int[,] inputcells){
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				this.cell [j, i] = inputcells [4 - j, i];
			}
		}
	}
}