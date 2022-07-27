import 'package:intl/intl.dart';

formatNumber(dynamic) {
  var formatter = NumberFormat("###.00");
  if (dynamic == null) return "";
  return formatter.format(dynamic);
}
