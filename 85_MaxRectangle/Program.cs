using System.Diagnostics;

char[][] input1 = new char[][]
{
    new char[]{'1','0','1','0','0'},
    new char[]{'1','0','1','1','1'},
    new char[]{'1','1','1','1','1'},
    new char[]{'1','0','0','1','0'}
};
var output1 = 6;

char[][] input2 = new char[][]{new char[]  {'0'}};
var output2 = 0;    

char[][] input3 = new char[][]{new char[]  {'1'}};
var output3 = 1;

char[][] input4 = new char[][]{
    new char[] {'1','1','1','1','1','1','1','1'},
    new char[] {'1','1','1','1','1','1','1','0'},
    new char[] {'1','1','1','1','1','1','1','0'},
    new char[] {'1','1','1','1','1','0','0','0'},
    new char[] {'0','1','1','1','1','0','0','0'}
};
var output4 = 21;
var s = new Solution();


Console.WriteLine(output4 == s.MaximalRectangle(input4)); 
Console.WriteLine(output1 == s.MaximalRectangle(input1));
Console.WriteLine(output2 == s.MaximalRectangle(input2));
Console.WriteLine(output3 == s.MaximalRectangle(input3));




public class Solution {

    struct Rect
    {
        private readonly char[][] matrix;
        private readonly int i1;
        private readonly int j1;
        private readonly int i2;
        private readonly int j2;
        public Rect(char[][] matrix, int i1, int j1)
        {
            this.matrix = matrix;
            this.i1 = i1;
            this.j1 = j1;
            this.i2 = i1;
            this.j2 = j1;
        }

        public Rect(char[][] matrix, int i1, int j1, int i2, int j2)
        {
            this.matrix = matrix;
            this.i1 = i1;
            this.j1 = j1;
            this.i2 = i2;
            this.j2 = j2;
        }

        public int Area() => (i2 - i1 + 1) * (j2 - j1 + 1);

        public bool CanExtendRight()
        {
            if (j2 == matrix[0].Length - 1)
            {
                return false;
            }

            for(int i=i1; i<=i2; i++)
            {
                if (matrix[i][j2 + 1] == '0')
                {
                    return false;
                }
            }

            return true;
        }

        public Rect ExtendRight()
        {
            return new Rect(matrix, i1, j1, i2, j2 + 1);
        }

        public bool CanExtendDown()
        {
            if (i2 == matrix.Length - 1)
            {
                return false;
            }

            for (int j = j1; j <= j2; j++)
            {
                if (matrix[i2 + 1][j] == '0') 
                {
                    return false;
                }
            }

            return true;
        }

        public Rect ExtendDown()
        {
            return new Rect(matrix, i1, j1, i2 + 1, j2);
        }

        public bool CanExtendRightAndDown()
        {
            return CanExtendRight() && CanExtendDown() && matrix[i2+1][j2+1] == '1';
        }

        public Rect ExtendRightAndDown()
        {
            return new Rect(matrix, i1, j1, i2 + 1, j2 + 1);
        }

        public (int, int) Start()
        {
            return (i1,j1);
        }

        public bool Contains(int i, int j)
        {
            if (i1>=i &&  i2 <=i && j1 >= j && j2 <= j)
            {
                return true;
            }

            return false;
        }

    }

    int n;
    int m;
    char[][] matrix;


    public int MaximalRectangle(char[][] matrix) {
        this.matrix = matrix;
        n = matrix.Length;
        m = matrix[0].Length;
        int max = 0;
        Rect? maxRect = null;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                
                if (matrix[i][j] == '1')
                {
                    if (maxRect != null && maxRect.Value.Contains(i, j))
                    {
                        continue;
                    }
                    var currentRect = MaxArea(new Rect(matrix, i, j));
                    if (currentRect.Area() > max)
                    {
                        max = currentRect.Area();
                        maxRect = currentRect;
                    }                    
                }
            }
        }
        return max;
        
    }

   


    private Rect MaxArea(Rect rect)
    {   
        Rect maxRect = rect;

        if (rect.CanExtendRightAndDown())
        {
            maxRect = Max(maxRect, MaxArea(rect.ExtendRightAndDown()));
        }

        if (rect.CanExtendRight())
        {
            maxRect = Max(maxRect, MaxArea(rect.ExtendRight()));
        }

        if (rect.CanExtendDown())
        {
            maxRect = Max(maxRect, MaxArea(rect.ExtendDown()));
        }       

        return maxRect;
    }

    private Rect Max(Rect rect1, Rect rect2)
    {
        if (rect1.Area() > rect2.Area())
        {
            return rect1;
        }

        return rect2;
    }
}

