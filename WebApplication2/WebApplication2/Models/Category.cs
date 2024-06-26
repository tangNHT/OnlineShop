﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WebApplication2.Models;

public partial class Category
{
	public int Id { get; set; }
	//Giới hạn độ dài của chuỗi
	[StringLength(50, ErrorMessage = "Bạn hãy nhập tên không quá 50 ký tự")]
	[DisplayName("Tên danh mục")]
	[Required(ErrorMessage = "Bạn hãy nhập tên danh mục")]
	public string Name { get; set; }
	[DisplayName("Tiêu đề SEO")]
	public string Alias { get; set; }
	[DisplayName("Danh mục cha")]
	public int ParentId { get; set; }
	[DisplayName("Ngày tạo")]
	public DateTime? CreateDate { get; set; }
	[DisplayName("Thứ tự")]
	[Range(0, Int32.MaxValue, ErrorMessage = "Bạn hãy nhập một số")]
	public int? Order { get; set; }
	[DisplayName("Trạng thái")]
	public bool Status { get; set; }
	[DisplayName("Cần thiết")]
	public int? Strong { get; set; }
}