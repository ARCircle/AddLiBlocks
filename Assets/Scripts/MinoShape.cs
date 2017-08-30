public class MinoShape {
	public int[,] cell = new int[5,5];

	public MinoShape(){
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				cell[i,j] = 0;
			}
		}
	}
}


// (1,2) (2,-1) (-1,-2) (-2,1)

/*
ミノの形を保存する
保存したデータからインスタンス

*/