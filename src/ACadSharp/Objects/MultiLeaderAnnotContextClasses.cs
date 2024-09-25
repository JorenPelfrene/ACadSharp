﻿using System;
using System.Collections.Generic;
using System.Linq;
using ACadSharp.Attributes;
using ACadSharp.Tables;

using CSMath;


namespace ACadSharp.Objects
{

	/// <summary>
	/// Nested classes in MultiLeaderAnnotContext
	/// </summary>
	public partial class MultiLeaderAnnotContext : NonGraphicalObject
	{
		/// <summary>
		/// Represents a leader root
		/// </summary>
		/// <remarks>
		/// Appears in DXF as 302 DXF: “LEADER“
		/// </remarks>
		public class LeaderRoot : ICloneable
		{
			public LeaderRoot() { }

			/// <summary>
			/// Is content valid (ODA writes true)
			/// </summary>
			[DxfCodeValue(290)]
			public bool ContentValid { get; set; }

			/// <summary>
			/// Unknown (ODA writes true)
			/// </summary>
			[DxfCodeValue(291)]
			public bool Unknown { get; set; }

			/// <summary>
			/// Connection point
			/// </summary>
			[DxfCodeValue(10, 20, 30)]
			public XYZ ConnectionPoint { get; set; }

			/// <summary>
			/// Direction
			/// </summary>
			[DxfCodeValue(11, 21, 31)]
			public XYZ Direction { get; set; }

			/// <summary>
			/// Gets a list of <see cref="StartEndPointPair" />.
			/// </summary>
			public IList<StartEndPointPair> BreakStartEndPointsPairs { get; } = new List<StartEndPointPair>();

			/// <summary>
			/// Leader index
			/// </summary>
			[DxfCodeValue(90)]
			public int LeaderIndex { get; set; }

			/// <summary>
			/// Landing distance
			/// </summary>
			[DxfCodeValue(40)]
			public double LandingDistance { get; set; }

			/// <summary>
			/// Gets a list of <see cref="LeaderLine"/> objects representing
			/// leader lines starting from the landing point
			/// of the multi leader.
			/// </summary>
			public IList<LeaderLine> Lines { get; } = new List<LeaderLine>();

			//R2010
			/// <summary>
			/// Attachment direction
			/// </summary>
			[DxfCodeValue(271)]
			public TextAttachmentDirectionType TextAttachmentDirection { get; set; }

			public object Clone()
			{
				LeaderRoot clone = (LeaderRoot)this.MemberwiseClone();

				foreach (var breakStartEndPoint in BreakStartEndPointsPairs.ToList())
				{
					clone.BreakStartEndPointsPairs.Add((StartEndPointPair)breakStartEndPoint.Clone());
				}

				foreach (var line in Lines.ToList())
				{
					clone.Lines.Add((LeaderLine)line.Clone());
				}

				return clone;
			}
		}

		/// <summary>
		/// Start/end point pairs
		/// 3BD	11	Start Point
		/// 3BD	12	End point
		/// </summary>
		public struct StartEndPointPair : ICloneable
		{
			public StartEndPointPair(XYZ startPoint, XYZ endPoint) {
				StartPoint = startPoint;
				EndPoint = endPoint;
			}

			/// <summary>
			/// Break start point
			/// </summary>
			[DxfCodeValue(12, 22, 32)]
			public XYZ StartPoint { get; private set; }

			/// <summary>
			/// Break end point
			/// </summary>
			[DxfCodeValue(13, 23, 33)]
			public XYZ EndPoint { get; private set; }

			public object Clone()
			{
				return this.MemberwiseClone();
			}
		}


		/// <summary>
		///	Represents a leader line
		/// </summary>
		/// <remarks>
		/// Appears as 304	DXF: “LEADER_LINE“
		/// </remarks>
		public class LeaderLine : ICloneable
		{
			public LeaderLine() { }

			/// <summary>
			/// Get the list of points of this <see cref="LeaderLine"/>.
			/// </summary>
			public IList<XYZ> Points { get; } = new List<XYZ>();

			/// <summary>
			/// Break info count
			/// </summary>
			public int BreakInfoCount { get; set; }

			/// <summary>
			/// Segment index
			/// </summary>
			[DxfCodeValue(90)]
			public int SegmentIndex { get; set; }

			/// <summary>
			/// Start/end point pairs
			/// </summary>
			public IList<StartEndPointPair> StartEndPoints { get; } = new List<StartEndPointPair>();

			/// <summary>
			/// Leader line index.
			/// </summary>
			[DxfCodeValue(91)]
			public int Index { get; set; }

			//R2010
			/// <summary>
			/// Leader type
			/// </summary>
			[DxfCodeValue(170)]
			public MultiLeaderPathType PathType { get; set; }

			/// <summary>
			/// Line color
			/// </summary>
			[DxfCodeValue(92)]
			public Color LineColor { get; set; }

			/// <summary>
			/// Line type
			/// </summary>
			[DxfCodeValue(340)]
			public LineType LineType { get; set; }

			/// <summary>
			/// Line weight
			/// </summary>
			[DxfCodeValue(171)]
			public LineWeightType LineWeight { get; set; }

			/// <summary>
			/// Arrowhead size
			/// </summary>
			[DxfCodeValue(40)]
			public double ArrowheadSize { get; set; }

			/// <summary>
			/// Gets or sets a <see cref="BlockRecord"/> containig elements
			/// to be dawn as arrow symbol.
			/// </summary>
			[DxfCodeValue(341)]
			public BlockRecord Arrowhead { get; set; }

			/// <summary>
			/// Override flags
			/// </summary>
			[DxfCodeValue(93)]
			public LeaderLinePropertOverrideFlags OverrideFlags { get; set; }

			public object Clone()
			{
				LeaderLine clone = (LeaderLine)this.MemberwiseClone();

				foreach (var point in Points.ToList())
				{
					clone.Points.Add(point);
				}

				foreach (var startEndPoint in StartEndPoints.ToList())
				{
					clone.StartEndPoints.Add((StartEndPointPair)startEndPoint.Clone());
				}

				return clone;
			}
		}
	}
}