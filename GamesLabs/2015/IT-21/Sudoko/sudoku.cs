using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Text;

namespace Sudoku
{
	public enum GameLevel
	{
	   SIMPLE,
	   MEDIUM,
	   COMPLEX
	
	
	}
	enum  GameCombinations
	{
	   SWAP_ROWS,
	   SWAP_COLS,
	   SWAP_SETS,
	   REVERSE_ROW_OR_COL
	};

	public class Sudoku
	{
		

		public Sudoku()
		{
		  
		  _numberSet = new int[MAX_ROWS,MAX_COLS];
		  
		  _problemSet =new int[MAX_ROWS,MAX_COLS];
		  
			
		  _problemSetCopy =new int[MAX_ROWS,MAX_COLS];
		}


		public DataSet GameSet
		{
		
			get{return FormDataSet();}


		}

		public void GenerateGame(GameLevel level)
		{
		
			InitialiseSet();
			int minPos,maxPos,noOfSets;
			 
			
			switch(level)
			{
			
				case GameLevel.SIMPLE:
					minPos=4;
					maxPos=6;
					noOfSets=8;
					UnMask(minPos,maxPos,noOfSets);
					break;
				case GameLevel.MEDIUM:
					minPos=3;
					maxPos=5;
					noOfSets= 7;
					UnMask(minPos,maxPos,noOfSets);
					break;
				case GameLevel.COMPLEX:
					 minPos=3;
					 maxPos=5;
					noOfSets = 6;
					UnMask(minPos,maxPos,noOfSets);
					 break;
				default:
					 UnMask(3,6,7);
					  break;
			}
			
			for(int i=0;i<MAX_ROWS;i++)
			{
				for(int j=0;j<MAX_COLS;j++)
				{
				
					_problemSetCopy[i,j] =_problemSet[i,j];
				}
			}

		
		
		}

		private void UnMask(int minPos,int maxPos,int noOfSets)
		{
			int seed;
			int [] posX = {0,0,0,1,1,1,2,2,2};
			int [] posY = {0,1,2,0,1,2,0,1,2};
			int [] maskedSet={0,0,0,0,0,0,0,0,0};
			Random number;
		    int setCount =0;
			do
			{

				seed= DateTime.Now.Millisecond;
				number = new Random(seed);
				int i= number.Next(0,9);
				
				if(maskedSet[i] ==0)
				{
					maskedSet[i]=1;
					setCount++;
				    seed = DateTime.Now.Millisecond;
					number = new Random(seed);
					int maskPos = number.Next(minPos,maxPos);
					int j=0;
					do
					{
						seed  = DateTime.Now.Millisecond;
						number = new Random(seed);
						int newPos = number.Next(1,9);
						int x = _setRowPosition[i]+posX[newPos];
						int y=  _setColPosition[i]+posY[newPos];
						if(_problemSet[x,y]==0)
						{
							_problemSet[x,y] =_numberSet[x,y];
							j++;
						}
				
					}while(j<maskPos);
			
			
				}
			}while(setCount < noOfSets);

		}



		public bool InitiliseExistingGame(DataSet gameSet)
		{
		     
			try
			{
				DataTable currentTable = gameSet.Tables["answerset"];
				int i=0,j=0;
				string colname=null;
				foreach(DataRow row in currentTable.Rows)
				{
					for(j=0;j<MAX_COLS;j++)
					{

						colname = "col" +j.ToString().TrimEnd();
					    string dataValue = row[colname] as string;
						if(dataValue == null)
							continue;

						if(dataValue.TrimEnd()!="")
						{
							_numberSet[i,j]= Int32.Parse(dataValue);
						}
						else
						{
							_numberSet[i,j]=0;
						}
						
					}
						i++;
				}

				i=j=0;
				currentTable = gameSet.Tables["numberset"];
				foreach(DataRow row in currentTable.Rows)
				{
					for(j=0; j<MAX_COLS;j++)
					{
						colname = "col" +j.ToString().TrimEnd();
					
						string dataValue = row[colname] as string;
						if(dataValue == null)
							continue;

						if(dataValue.TrimEnd()!="")
						{
							_problemSet[i,j]= Int32.Parse(dataValue);
						}
						else
						{
							_problemSet[i,j]=0;
						}
						
						
					}
					i++;
				}

				i=j=0;
				currentTable = gameSet.Tables["problemcopyset"];
				foreach(DataRow row in currentTable.Rows)
				{
					for(j=0;j<MAX_COLS;j++)
					{
						colname = "col" +j.ToString().TrimEnd();
					
						string dataValue = row[colname] as string;
						if(dataValue == null)
							continue;

						if(dataValue.TrimEnd()!="")
						{
							_problemSetCopy[i,j]= Int32.Parse(dataValue);
						}
						else
						{
							_problemSetCopy[i,j]=0;
						}
					
						
					}
					i++;
				}

				return true;
			
			}
			catch(Exception ex)
			{
			  Console.WriteLine("Error occured while initilising game and error is {0}",ex.Message);
				return false;
			}
			
		
		}
		

		public bool CheckForDuplicate(int rowPos,int colPos,int currentValue)
		{
			for(int i=0;i<MAX_ROWS;i++)
			{
				
				if(_problemSet[i,colPos]== currentValue)
				{
					if(i!=rowPos)
					{
						return true;
					}else{
						continue;}
						
				}
				if(_problemSet[rowPos,i] == currentValue)
				{
					if(i!= colPos){
						return true;}else{
						continue;}
				}

			}

				int x = rowPos /3;
				int y = colPos/3;
				for(int j=0;j<MINI_SET_ROWS;j++)
				{
					for(int k=0;k<MINI_SET_COLS;k++)
					{
						int p = x*3+j;
						int q = y*3+k;

						if ((p==rowPos)&&(q==colPos))
						{
							continue;
						}else if(_problemSet[p,q]== currentValue)
						{
						    return true;
						}
					
					}

				}
			
				return false;
			
		
		
		}

		public bool CheckIfAnswerPosition(int rowPos,int colPos,int dataValue)
		{
		
			
				if(_problemSetCopy[rowPos,colPos]!=0)
					return true;
			    else
					 return false;
			
		
		
		
		}

		public bool CheckForAnswerChange(int rowPos,int colPos,int currentValue)
		{
			if(_problemSetCopy[rowPos,colPos]!=0)
			{
				if(_problemSetCopy[rowPos,colPos] != currentValue)
				{
				   return true;
				}
			}
			return false;
		
		}

		private DataSet FormDataSet()
		{
		
			try
			{
				DataSet ds = new DataSet("sudokuset");

				StringBuilder sb = new StringBuilder();
				sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
				sb.Append("<sudokuset>");
				sb.Append("<numbersets>");

				for(int i=0;i<MAX_ROWS;i++)
				{
					sb.Append("<numberset>");
					for(int j=0;j<MAX_COLS;j++)
					{
					
						sb.Append("<col"+j.ToString().TrimEnd()+">");
						if(_problemSet[i,j]==0)
							sb.Append(" ");
						else
							sb.Append(_problemSet[i,j].ToString().TrimEnd());
						sb.Append("</col"+j.ToString().TrimEnd()+">");
						sb.Append("\n");
				
					}
					sb.Append("</numberset>");
					sb.Append("\n");
				}
				sb.Append("</numbersets>");

				sb.Append("<problemcopysets>");

				for(int i=0;i<MAX_ROWS;i++)
				{
					sb.Append("<problemcopyset>");
					for(int j=0;j<MAX_COLS;j++)
					{
					
						sb.Append("<col"+j.ToString().TrimEnd()+">");
						if(_problemSetCopy[i,j]==0)
							sb.Append(" ");
						else
							sb.Append(_problemSetCopy[i,j].ToString().TrimEnd());
						sb.Append("</col"+j.ToString().TrimEnd()+">");
						sb.Append("\n");
				
					}
					sb.Append("</problemcopyset>");
					sb.Append("\n");
				}
				sb.Append("</problemcopysets>");

				sb.Append("<answersets>");

				for(int i=0;i<MAX_ROWS;i++)
				{
					sb.Append("<answerset>");
					for(int j=0;j<MAX_COLS;j++)
					{
					
						sb.Append("<col"+j.ToString().TrimEnd()+">");
						if(_numberSet[i,j]==0)
							sb.Append(" ");
						else
							sb.Append(_numberSet[i,j].ToString().TrimEnd());
						sb.Append("</col"+j.ToString().TrimEnd()+">");
						sb.Append("\n");
				
					}
					sb.Append("</answerset>");
					sb.Append("\n");
				}
				sb.Append("</answersets>");

				sb.Append("</sudokuset>");

			
				StringReader sr = new StringReader(sb.ToString());
			  
				ds.ReadXml(sr);
			  
			  
				return ds;
			}
			catch(Exception ex)
			{
				Console.WriteLine("Error ocurred while forming dataset and is {0}",ex.Message);
				return null;
			}
		
		}
		private void InitialiseSet()
		{
			int seed = DateTime.Now.Millisecond %3;
			

			for(int i=0;i<MAX_ROWS;i++)
			{
				for(int j=0;j<MAX_COLS;j++)
				{
				
				  _numberSet[i,j] =_originalSet[i,j];
				  _problemSet[i,j]=0;
				  _problemSetCopy[i,j]=0;
				}
			}
			 Random number = new Random(seed);
			 int roworcolPos = number.Next(1,3);
			  seed = DateTime.Now.Millisecond %3;
			  number = new Random(seed);
			 int setNumber = number.Next(1,3);
			if(_swapRows)
			{
				  SwapData(setNumber,roworcolPos,GameCombinations.SWAP_ROWS);
				_swapRows =false;  
			}
			else
			{
				  SwapData(setNumber,roworcolPos,GameCombinations.SWAP_COLS);
				_swapRows=true;
			}
			 
               seed = DateTime.Now.Millisecond %3;
			    number = new Random(seed);
			   setNumber = number.Next(1,3);
			     SwapData(setNumber,roworcolPos,GameCombinations.SWAP_SETS);

		
             
		}
		private void SwapData(int setNumber,int roworcolPos,GameCombinations swapType)
		{
			
			int x1=0,x2=0,y1=0,y2=0;
			int i=0,j=0;
			switch(swapType)
			{
				case GameCombinations.SWAP_COLS:
					  y1= _setColPosition[setNumber*3]+roworcolPos;
					if(roworcolPos==2)
					{
						y2 =_setColPosition[setNumber*3];
					}
					else
					{
						y2 = y1+1;
					}
					for(i=0;i<MAX_ROWS;i++)
					{
						
					    _numberSet[i,y2] = _originalSet[i,y1];
						_numberSet[i,y1] = _originalSet[i,y2];
					
					}
					break;
				case GameCombinations.SWAP_ROWS:
					x1= _setRowPosition[setNumber*3]+roworcolPos;
					if(roworcolPos==2)
					{
						x2 =_setRowPosition[setNumber*3];
					}
					else
					{
						x2 =x1+1;
					}
					for(i=0;i<MAX_COLS;i++)
					{
						
						_numberSet[x2,i] = _originalSet[x1,i];
						_numberSet[x1,i] = _originalSet[x2,i];
					
					}
					break;

				case GameCombinations.SWAP_SETS:
					if(_swapRows)
					{
						x1 = setNumber;
						if(setNumber == 2)
							x2 =0;
						else
							x2 = x1+1;

						for(j=0;j<MAX_COLS;j++)
						{
							for(i=0;i<MINI_SET_ROWS;i++)
							{
								int temp =_numberSet[x2*3+i,j];
								_numberSet[x2*3+i,j] =_numberSet[x1*3+i,j];
								_numberSet[x1*3+i,j] =temp;
								
							}
						}
					  
					}
					else
					{
						y1 = setNumber;
						if(setNumber == 2)
							y2 =0;
						else
							y2 = y1+1;

						for(j=0;j<MAX_ROWS;j++)
						{
							for(i=0;i<MINI_SET_COLS;i++)
							{
								int temp = _numberSet[j,y1*3+i];
								_numberSet[j,y1*3+i] =_numberSet[j,y2*3+i];
								_numberSet[j,y2*3+i] = temp;
							}
						}
					
					
					
					}
					  break;
				default:
					break;
							
				
				
			
			
			}
		
		
		
		}
		private bool SwapNumberSet(int x1,int y1, int x2,int y2,int roworcol)
		{
			int n1,n2,n3,n4,cnt=0;
			if(roworcol==1)
			{
				n1 = _numberSet[x1,y1];
				n2 = _numberSet[x2,y1];
				n3 = _numberSet[x2,y2];
				n4 = _numberSet[x1,y2];
				_numberSet[x1,y1]=n2;
				_numberSet[x2,y1]=n1;
				_numberSet[x2,y2] =n4;
				_numberSet[x1,y2] =n3;

			}
			else
			{
				n1 =_numberSet[x1,y1];
				n2 =_numberSet[x1,y2];
				n3 =_numberSet[x2,y1];
				n4 =_numberSet[x2,y2];
				_numberSet[x1,y1]=n2;
				_numberSet[x1,y2]=n1;
				_numberSet[x2,y1] =n4;
				_numberSet[x2,y2]=n3;
			}
		  
			if(roworcol ==1)
			{
				for(int i=1; i<=MAX_ROWS;i++)
				{
					cnt =0;
					for(int j=0;j<MAX_COLS;j++)
					{
				
						if(_numberSet[x1,j]==i)
							cnt++;
					}
					if(cnt > 1)
					{
						_numberSet[x1,y1]=n1;
						_numberSet[x2,y1]=n2;
						_numberSet[x2,y2]=n3;
						_numberSet[x1,y2]=n4;
          
						return false;
					}
				}

				for(int i=1; i<=MAX_ROWS;i++)
				{
					cnt =0;
					for(int j=0;j<MAX_COLS;j++)
					{
				
						if(_numberSet[x2,j]==i)
							cnt++;
					}
					if(cnt > 1)
					{
						_numberSet[x1,y1]=n1;
						_numberSet[x2,y1]=n2;
						_numberSet[x2,y2]=n3;
						_numberSet[x1,y2]=n4;
          
						return false;
					}
				}

			}
			else
			{
				for(int i=1; i<=MAX_ROWS;i++)
				{
					cnt =0;
					for(int j=0;j<MAX_ROWS;j++)
					{
				
						if(_numberSet[j,y1]==i)
							cnt++;
					}
					if(cnt > 1)
					{
						_numberSet[x1,y1]=n1;
						_numberSet[x1,y2]=n2;
						_numberSet[x2,y1]=n3;
						_numberSet[x2,y2]=n4;
          
						return false;
				
					}	
				}

				for(int i=1; i<=MAX_ROWS;i++)
				{
					cnt =0;
					for(int j=0;j<MAX_ROWS;j++)
					{
				
						if(_numberSet[j,y2]==i)
							cnt++;
					}
					if(cnt > 1)
					{
						_numberSet[x1,y1]=n1;
						_numberSet[x1,y2]=n2;
						_numberSet[x2,y1]=n3;
						_numberSet[x2,y2]=n4;
          
						return false;
					}
					
				}
			
			
			}

		  
			return true;
		}

		private bool SwapNumber(int pos,int number,int set1,int setNumber)
		{
			int [] xpos = {0,0,0,1,1,1,2,2,2};
			int [] ypos = {0,1,2,0,1,2,0,1,2};
			int x=0,y=0,x1,y1;
			bool duplicate=false;
			for(int i=0;i<MAX_ROWS;i++)
			{
				duplicate=false;

				if(i !=pos)
				{
					x = _setRowPosition[setNumber]+ xpos[i];
					y = _setColPosition[setNumber]+ ypos[i];
				
					duplicate=false;
					for(int j=0;j<MAX_COLS;j++)
					{
						if((_numberSet[x,j] ==number)||(_numberSet[j,y]==number))
						{
							duplicate =true;
							j=MAX_COLS;
						}
					}
					if(!duplicate)
					{
				
						int newNumber = _numberSet[x,y];
						x1 = _setRowPosition[setNumber]+ xpos[pos];
						y1 = _setColPosition[setNumber]+ ypos[pos];
						_numberSet[x,y]=0;
						for(int j=0;j<MAX_COLS;j++)
						{
							if((_numberSet[x1,j] ==newNumber)||(_numberSet[j,y1]==newNumber))
							{
								duplicate =true;
								_numberSet[x,y]=newNumber;
								j=MAX_COLS;
							}
						}

						if(!duplicate)
						{
							_numberSet[x,y] =number;
							_numberSet[x1,y1]=newNumber;
							return true;
						}
					}

				}
				
			}
		
			return false;
		
		}

		bool _swapRows =true;
		private int[,] _originalSet = {{7,9,2,3,5,1,8,4,6},
									 {4,6,8,9,2,7,5,1,3},
									 {1,3,5,6,8,4,7,9,2},
									 {6,2,1,5,7,9,4,3,8},
									 {5,8,3,2,4,6,1,7,9},
									 {9,7,4,8,1,3,2,6,5},
									 {8,1,6,4,9,2,3,5,7},
									 {3,5,7,1,6,8,9,2,4},
									 {2,4,9,7,3,5,6,8,1}
									};
		private int[,]_numberSet;
		private int[,]_problemSet;
		private int[,]_problemSetCopy;
		private int[]  _setRowPosition = {0,0,0,3,3,3,6,6,6};
		private int[]  _setColPosition = {0,3,6,0,3,6,0,3,6};

	
		private const int MAX_ROWS =9;
		private const int MAX_COLS=9;
		private const int MINI_SET_ROWS =3;
		private const int MINI_SET_COLS =3;
		

	}
}
