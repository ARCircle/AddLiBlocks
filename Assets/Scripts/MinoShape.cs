public class MinoShape {
	public int[,] cell = new int[5,5];
	public int[,] nowcell = new int[5,5];
	int rot = 0; //0, 1, 2, 3

	public MinoShape(){
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				cell[i,j] = 0;
			}
		}
	}
	public void rotate(int degree){ //degree = 0, 1, 2, 3
		rot = (rot + degree) % 4;
		nowcell [2, 2] = cell [2, 2];
		int xnow = 0, ynow = 0, xnext = 0, ynext = 0, tmp = 0;
		int[] xs = { 0, 1, 2, 0, 1, 2 };
		int[] ys = { 1, 1, 1, 2, 2, 2 };
		for (int i = 0; i < 6; i++) {
			xnow = xs [i];
			ynow = ys [i];
			tmp = cell [xnow, ynow];
			for (int j = 0; j < 3; j++) {
				xnext = -ynow;
				ynext = xnow;
				nowcell [xnow, ynow] = cell [xnext, ynext];
				xnow = xnext;
				ynow = ynext;
			}
			cell [xnow, ynow] = tmp;
		}
	}
}


// (1,2) (2,-1) (-1,-2) (-2,1)

/*
ミノの形を保存する
保存したデータからインスタンス

*/